using Gemfruit.Mod.API;
using Gemfruit.Mod.Internal;
using StardewValley.Monsters;

namespace Gemfruit.Mod.Monsters
{
    public delegate Monster MineshaftConstructor(MineshaftSpawnData mineshaftSpawnData);

    public delegate Monster WildernessConstructor(WildernessSpawnData wildernessSpawnData);
    
    public class MonsterType
    {
        private ResourceKey name;
        private MineshaftConstructor _mineshaftConstructor;
        private WildernessConstructor _wildernessConstructor;

        public MonsterType(ResourceKey name)
        {
            this.name = name;
            _mineshaftConstructor =
                _ =>
                {
                    GemfruitMod.Logger.Log(LogLevel.Error, "MonsterType",
                        $"Can't spawn '{name}' - no mineshaft constructor!");
                    return null;
                };
            _wildernessConstructor =
                _ =>
                {
                    GemfruitMod.Logger.Log(LogLevel.Error, "MonsterType",
                        $"Can't spawn '{name}' - no wilderness constructor!");
                    return null;
                };
        }


        public MonsterType SetMineshaftConstructor(MineshaftConstructor constructor)
        {
            _mineshaftConstructor = constructor;
            return this;
        }

        public MonsterType SetWildernessConstructor(WildernessConstructor constructor)
        {
            _wildernessConstructor = constructor;
            return this;
        }

        public MineshaftConstructor GetMineshaftConstructor()
        {
            return _mineshaftConstructor;
        }

        public WildernessConstructor GetWildernessConstructor()
        {
            return _wildernessConstructor;
        }

        public ResourceKey GetName()
        {
            return name;
        }
    }
}