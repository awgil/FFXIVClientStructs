// <auto-generated/>
namespace FFXIVClientStructs.FFXIV.Component.Excel.Sheets;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x40C)]
public unsafe partial struct SequentialEvent {
    [FieldOffset(0x00), FixedSizeArray] internal FixedSizeArray64<UnknownStructStruct> _unknownStruct;
    [FieldOffset(0x400)] public uint Unknown320;
    [FieldOffset(0x404)] public uint Unknown_70;
    [FieldOffset(0x408)] public ushort Unknown321;

    [StructLayout(LayoutKind.Explicit, Size = 0x10)]
    public partial struct UnknownStructStruct {
        [FieldOffset(0x00)] public uint Unknown1;
        [FieldOffset(0x04)] public uint Unknown2;
        [FieldOffset(0x08)] public ushort Unknown3;
        [FieldOffset(0x0A)] public ushort Unknown4;
        [FieldOffset(0x0C)] public ushort Unknown5;
    }
}