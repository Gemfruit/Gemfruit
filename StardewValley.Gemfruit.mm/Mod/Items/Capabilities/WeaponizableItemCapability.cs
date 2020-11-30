using System;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class WeaponizableItemCapability : ItemCapability
    {
        public int MinimumDamage { get; }
        public int MaximumDamage { get; }
        public float Knockback { get; }
        public int Speed { get; }
        public int Precision { get; }
        public int Defense { get; }
        public WeaponType WeaponType { get; }
        public int BaseLevel { get; }
        public int MinimumLevel { get; }
        public int AreaOfEffect { get; }
        public float CritChance { get; }
        public float CritMultiplier { get; }
        
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

        

        public WeaponizableItemCapability(int minimumDamage, int maximumDamage, float knockback, int speed, 
            int precision, int defense, WeaponType weaponType, int baseLevel, int minimumLevel, 
            int areaOfEffect, float critChance, float critMultiplier)
        {
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            Knockback = knockback;
            Speed = speed;
            Precision = precision;
            Defense = defense;
            WeaponType = weaponType;
            BaseLevel = baseLevel;
            MinimumLevel = minimumLevel;
            AreaOfEffect = areaOfEffect;
            CritChance = critChance;
            CritMultiplier = critMultiplier;
        }
    }
}