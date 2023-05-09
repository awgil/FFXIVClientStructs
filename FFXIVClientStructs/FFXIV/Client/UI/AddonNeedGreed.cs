using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.FFXIV.Client.UI;

[Addon("NeedGreed")]
[StructLayout(LayoutKind.Explicit, Size = 0x510)]
public unsafe partial struct AddonNeedGreed
{
    [FieldOffset(0x00)] public AtkUnitBase AtkUnitBase;

    // Addon has 32 pre-allocated list item renderers
    [FixedSizeArray<LootItemInfo>(16)]
    [FieldOffset(0x220)] public fixed byte Items[0x28 * 16];
    
    // Contains other information for each loot item,
    // Such as time remaining, and a pointer to what is probably the tooltip string
    [StructLayout(LayoutKind.Explicit, Size = 0x28)]
    public struct LootItemInfo
    {
        [FieldOffset(0x08)] public uint ItemId;
    }
}