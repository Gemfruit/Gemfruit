using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Capabilities    
{
    public class IncubatableItemCapability : ItemCapability
    {
        private ResourceKey _animalToSpawn;
        private int _timeToReady;

        public IncubatableItemCapability(ResourceKey animalToSpawn, int timeToReady)
        {
            _animalToSpawn = animalToSpawn;
            _timeToReady = timeToReady;
        }

        public virtual ResourceKey GetAnimalToSpawn(IHasContainers containers)
        {
            return _animalToSpawn;
        }

        public virtual int GetTimeToReady(IHasContainers containers)
        {
            return _timeToReady;
        }
    }
}