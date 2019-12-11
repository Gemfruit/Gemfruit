using System;
using Gemfruit.Mod;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Monsters;
using Microsoft.Xna.Framework;
using MonoMod;
using StardewValley.Monsters;

#pragma warning disable 108,114,626,649
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace StardewValley.Locations
{
    internal class patch_MineShaft : MineShaft
    {
        private Random mineRandom;
        private bool isSlimeArea;
        private bool isDinoArea;
        
        [MonoModPublic]
        public bool ghostAdded;

        private static Vector2 tileToVector(int xTile, int yTile)
        {
            return new Vector2(xTile, yTile) * 64f;
        }

        private MineshaftArea getAreaEnum()
        {
            if (isSlimeArea) return MineshaftArea.SlimeArea;
            if (isDinoArea) return MineshaftArea.DinoArea;
            switch (getMineArea())
            {
                case 0:
                    return MineshaftArea.Surface;
                case 10:
                    return MineshaftArea.Underground;
                case 40:
                    return MineshaftArea.Ice;
                case 80:
                    return MineshaftArea.Lava;
                case 121:
                    return MineshaftArea.SkullCaverns;
                case 77377:
                    return MineshaftArea.Quarry;
                default:
                    return MineshaftArea.DinoArea;
            }
        }

        [MonoModReplace]
        public Monster getMonsterForThisLevel(int level, int xTile, int yTile)
        {
            var currentArea = getAreaEnum();
            var position = tileToVector(xTile, yTile);

            var spawnedEntity = GemfruitMod.MineshaftSpawnRegistry.Get(currentArea, this, mineRandom, level, xTile, yTile);

            if (spawnedEntity.IsPresent())
            {
                var monsterConstructor = GemfruitMod.MineshaftMonsterRegistry.Get(spawnedEntity.Unwrap());
                if (monsterConstructor.IsPresent())
                {
                    return monsterConstructor.Unwrap()(new MineshaftMonsterRegistry.SpawnData(this, mineRandom, position, getMineArea(), level));
                }
                Console.Error.WriteLine($"Tried to spawn '{spawnedEntity.Unwrap()}', but was unable to find a registered constructor!");
            }

            Console.Error.WriteLine($"Exhausted spawn pool for area '{currentArea}' - defaulting to caution slime!!!");
            return new GreenSlime(position, Color.Red);
        }
        
    }

    public static class MineShaftExt
    {
        public static void setGhostAdded(this MineShaft self, bool ghostAdded)
        {
            GemfruitMod.Logger.Log(LogLevel.DEBUG, "MineShaftExt", $"GhostAdded set to {ghostAdded}");
            ((patch_MineShaft) self).ghostAdded = ghostAdded;
        }

        public static bool wasGhostAdded(this MineShaft self)
        {
            return ((patch_MineShaft) self).ghostAdded;
        }
    }
}