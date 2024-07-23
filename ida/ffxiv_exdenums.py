from urllib.request import urlopen, Request
from urllib.error import HTTPError, URLError
from io import BufferedReader
from enum import IntEnum
from zlib import decompress
from json import load, loads
from zipfile import ZipFile
from tempfile import TemporaryFile
from yaml import load as yload, Loader
from re import sub
from os import listdir, walk, getenv
from os.path import isdir, join
from luminapie.game_data import GameData, ParsedFileName
from luminapie.excel import ExcelListFile, ExcelHeaderFile
from abc import abstractmethod

import ida_enum
import idaapi


class ExcelDataHeader:
    def __init__(self, data):
        # type: (bytes) -> None
        self.data = data
        self.parse()

    def parse(self):
        # type: () -> None
        self.magic = self.data[0:4]
        self.version = int.from_bytes(self.data[4:6], "big")
        self.index_size = int.from_bytes(self.data[8:12], "big")

    def __repr__(self):
        # type: () -> str
        return "Data Header: {0}, version: {1}, index_size: {2}".format(
            self.magic,
            self.version,
            self.index_size,
        )

class ExcelDataOffset:
    def __init__(self, data):
        # type: (bytes) -> None
        self.data = data
        self.parse()

    def parse(self):
        # type: () -> None
        self.row_id = int.from_bytes(self.data[0:4], "big")
        self.offset = int.from_bytes(self.data[4:8], "big")

    def __repr__(self):
        # type: () -> str
        return "Data Offset: {0} -> {1}".format(self.row_id, self.offset)

class ExcelDataRowHeader:
    def __init__(self, index, data, offset, data_offset):
        self.index = index
        self.data = data
        self.data_size = int.from_bytes(data[offset:offset + 4], "big")
        self.row_count = int.from_bytes(data[offset + 4:offset + 6], "big")
        self.start_offset = offset + 6
        self.data_offset = data_offset

    def read_string(self, offset):
        string_offset = int.from_bytes(self.data[self.start_offset + offset : self.start_offset + offset + 4], "big") + self.start_offset + self.data_offset
        string = ""
        while (self.data[string_offset] != 0):
            string += chr(self.data[string_offset])
            string_offset += 1
        return string

    def __repr__(self):
        return f"Row {self.index}: size={self.data_size}, cnt={self.row_count}"

class ExcelDataHeaderFile:
    def __init__(self, data):
        # type: (list[bytes]) -> None
        raw_data = bytearray()
        for chunk in data:
            raw_data += chunk
        self.data = raw_data
        self.header: ExcelDataHeader = None
        self.parse()

    def parse(self):
        # type: () -> None
        self.header = ExcelDataHeader(self.data[0:32])
        if self.header.magic != b"EXDF":
            raise Exception(f"Invalid EXDF header: {self.header.magic}")
        count = int(self.header.index_size / 8)
        self.row_data: list[ExcelDataOffset] = []
        for i in range(count):
            self.row_data.append(ExcelDataOffset(self.data[32 + (i * 8) : 32 + ((i + 1) * 8)]))

f = open(join(getenv("APPDATA"), "XIVLauncher", "launcherConfigV3.json"), "r")
config = load(f)
f.close()

game_data = GameData(join(config["GamePath"], "game"))

class Sheet:
    def __init__(self, name):
        self.header = ExcelHeaderFile(game_data.get_file(ParsedFileName(f"exd/{name}.exh")))
        self.rows = []
        for page in self.header.pagination:
            data = ExcelDataHeaderFile(game_data.get_file(ParsedFileName(f"exd/{name}_{page.start_id}_en.exd")))
            for row in data.row_data:
                self.rows.append(ExcelDataRowHeader(row.row_id, data.data, row.offset, self.header.header.data_offset))

if ida_enum.get_enum("Row::ClassJob") == idaapi.BADADDR:
    e = ida_enum.add_enum(idaapi.BADADDR, "Row::ClassJob", 0)
    for row in Sheet("ClassJob").rows:
        ida_enum.add_enum_member(e, f"ClassJob_{row.read_string(4)}", row.index)

#action = Sheet("Action")
#for row in action.rows:
#    print(row.read_string(0))
