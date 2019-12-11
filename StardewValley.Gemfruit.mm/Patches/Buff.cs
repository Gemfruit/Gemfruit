using Gemfruit.Mod.Player;

#pragma warning disable 108,114,626,649
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global


namespace StardewValley
{
    public class patch_Buff : Buff
    {
        public patch_Buff(string description, int millisecondsDuration, string source, int index) : base(description, millisecondsDuration, source, index)
        {
        }

        public patch_Buff(int which) : base(which)
        {
        }

        public patch_Buff(int farming, int fishing, int mining, int digging, int luck, int foraging, int crafting, int maxStamina, int magneticRadius, int speed, int defense, int attack, int minutesDuration, string source, string displaySource) : base(farming, fishing, mining, digging, luck, foraging, crafting, maxStamina, magneticRadius, speed, defense, attack, minutesDuration, source, displaySource)
        {
        }

        public patch_Buff(BuffType type) : base((int) type)
        {
        }
    }
}