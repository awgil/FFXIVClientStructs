namespace FFXIVClientStructs.FFXIV.Client.Network;

// Client::Network::PacketDispatcher
//   Client::Network::Protocol::Zone::PacketReceiverCallbackInterface
//   Client::Network::Protocol::Chat::PacketReceiverCallbackInterface
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x8)]
public unsafe partial struct PacketDispatcher {
    [MemberFunction("48 89 5C 24 ?? 56 48 83 EC ?? 48 8B 0D ?? ?? ?? ?? 48 8B F2")]
    public partial void HandleSocialPacket(nint packet);

    [MemberFunction("E8 ?? ?? ?? ?? E9 ?? ?? ?? ?? 44 0F B6 4B ?? 4C 8D 43 18 0F B7 53 14")]
    public static partial void HandleEventPlayPacket(ulong objectId, uint eventId, ushort state, ulong a4, uint* payload, byte payloadSize);

    [MemberFunction("E8 ?? ?? ?? ?? EB 10 48 8B 0D")]
    public static partial void SendEventCompletePacket(uint eventId, ushort stage, byte a3, uint* args, byte numArgs, void* a6);
}
