using Gemfruit.Mod.Internal;
using StardewValley.Monsters;

namespace Gemfruit.Mod.Monsters
{
    public delegate Monster MineshaftConstructor(MineshaftSpawnData mineshaftSpawnData);

    public delegate Monster WildernessConstructor(WildernessSpawnData wildernessSpawnData);
    
    public class MonsterType
    {
        private RegistryKey name;
        private MineshaftConstructor _mineshaftConstructor;
        private WildernessConstructor _wildernessConstructor;

        public MonsterType(RegistryKey name)
        {
            this.name = name;
            _mineshaftConstructor =
                _ =>
                {
                    GemfruitMod.Logger.Log(LogLevel.ERROR, "MonsterType",
                        $"Can't spawn '{name}' - no mineshaft constructor!");
                    return null;
                };
            _wildernessConstructor =
                _ =>
                {
                    GemfruitMod.Logger.Log(LogLevel.ERROR, "MonsterType",
                        $"Can't spawn '{name}' - no wilderness constructor!");
                    return null;
                };
        }


        public MonsterType setMineshaftConstructor(MineshaftConstructor constructor)
        {
            _mineshaftConstructor = constructor;
            return this;
        }

        public MonsterType setWildernessConstructor(WildernessConstructor constructor)
        {
            _wildernessConstructor = constructor;
            return this;
        }

        public MineshaftConstructor getMineshaftConstructor()
        {
            return _mineshaftConstructor;
        }

        public WildernessConstructor GetWildernessConstructor()
        {
            return _wildernessConstructor;
        }

        public RegistryKey getName()
        {
            return name;
        }
    }
}