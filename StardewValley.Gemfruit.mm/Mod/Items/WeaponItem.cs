using System;
using Gemfruit.Mod.API.Utility;
using Netcode;
using StardewValley;

namespace Gemfruit.Mod.Items
{
    public class WeaponItem : Item
    {
        public int MinimumDamage { get; protected set; }
        public int MaximumDamage { get; protected set; }
        public float Knockback { get; protected set; }
        public int Speed { get; protected set; }
        public int Precision { get; protected set; }
        public int Defense { get; protected set; }
        public WeaponType WeaponType { get; protected set; }
        public int BaseLevel { get; protected set; }
        public int MinimumLevel { get; protected set; }
        public int AreaOfEffect { get; protected set; }
        public float CritChance { get; protected set; }
        public float CritMultiplier { get; protected set; }

        public override int StackSize => 1;

        public int ItemLevel
        {
            get
            {
                var damageAverage = (MaximumDamage + MinimumDamage) / 2;
                var speedModifier = 1.0 + 0.1 * (Math.Max(0, Speed) + (WeaponType == WeaponType.Dagger ? 15 : 0));
                var dmgSpeed = damageAverage * speedModifier;
                var bonus = Precision / 2 
                            + Defense 
                            + (CritChance - 0.02) * 100.0 
                            + (CritMultiplier - 3.0) * 20.0;
                var num = 0 + (int) dmgSpeed + (int) bonus;

                if (WeaponType == WeaponType.Club)
                    num /= 2;

                return num / 5 + 1;
            }

        }

        internal static Result<WeaponItem, Exception> ParseFromString(string line)
        {
            var i = new WeaponItem();
            var parts = line.Split('/');

            try
            {
                // index 0 - Name
                i.Name = parts[0];

                // index 1 - description
                i.Description = parts[1];

                // index 2 - minimum damage
                i.MinimumDamage = int.Parse(parts[2]);

                // index 3 - maximum damage
                i.MaximumDamage = int.Parse(parts[3]);

                // index 4 - knockback
                i.Knockback = float.Parse(parts[4]);

                // index 5 - speed
                i.Speed = int.Parse(parts[5]);

                // index 6 - precision
                i.Precision = int.Parse(parts[6]);

                // index 7 - defense
                i.Defense = int.Parse(parts[7]);

                // index 8 - weapon type
                i.WeaponType = (WeaponType) int.Parse(parts[8]);

                // index 9 - base level
                i.BaseLevel = int.Parse(parts[9]);

                // index 10 - minimum level
                i.MinimumLevel = int.Parse(parts[10]);

                // index 11 - area of effect
                i.AreaOfEffect = int.Parse(parts[11]);

                // index 12 - crit chance
                i.CritChance = float.Parse(parts[12]);

                // index 13 - crit multiplier
                i.CritMultiplier = float.Parse(parts[13]);

                // TODO: Better localization system.
                i.DisplayName = parts.Length > 14 ? parts[14] : i.Name;
                
                // calculate this once, rather than continually
                i.Price = i.ItemLevel * 100;
                
                // hardcoded defaults
                i.Edibility = 0;
                i.Type = "Weapon";
                i.Category = -98;
            }
            catch (Exception e)
            {
                return Result<WeaponItem, Exception>.FromException(e);
            }

            return Result<WeaponItem, Exception>.FromValue(i);
        }
    }
}