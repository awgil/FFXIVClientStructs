using FFXIVClientStructs.FFXIV.Application.Network;

namespace FFXIVClientStructs.FFXIV.Client.Network;

// Client::Network::NetworkModuleProxy
//   Client::System::Common::NonCopyable
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x20)]
public unsafe partial struct NetworkModuleProxy {
    [FieldOffset(0x08)] public NetworkModule* NetworkModule;
    [FieldOffset(0x10)] public NetworkModulePacketReceiverCallback* ReceiverCallback;

    [MemberFunction("E8 ?? ?? ?? ?? EB 0D 49 8B 87 ?? ?? ?? ??")]
    public partial bool IsInCrossWorldDuty();

    /// <summary>
    /// Gets current instance<br/>
    /// Does NOT invoke network request
    /// </summary>
    /// <returns>Current instance or 0 for non instanced zones</returns>
    [MemberFunction("E8 ?? ?? ?? ?? 48 8D 0D ?? ?? ?? ?? 0F B7 F0 E8 ?? ?? ?? ?? 8B D8")]
    public partial short GetCurrentInstance();

    [MemberFunction("E8 ?? ?? ?? ?? 44 89 6F 30")]
    public partial bool SendPacket2(void* packet, int a3, int a4);

    // TODO: not sure where to put all this
    [MemberFunction("E8 ?? ?? ?? ?? EB 10 48 8B 0D")]
    public static partial void SendEventDataPacket(uint eventId, ushort stage, byte a3, uint* args, byte numArgs, void* a6);

    [MemberFunction("E8 ?? ?? ?? ?? E9 ?? ?? ?? ?? 44 0F B6 4B ?? 4C 8D 43 18 0F B7 53 14")]
    public static partial void ProcessPacketEventPlay(ulong objectId, uint eventId, ushort state, ulong a4, uint* payload, byte payloadSize);
}
