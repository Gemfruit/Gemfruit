using Gemfruit.Mod.API.Exceptions;
using Gemfruit.Mod.Internal;

namespace Gemfruit.Mod.API.Utility.Registry
{
    public abstract class KeyedRegistry<T> : PhasableRegistry
    {
        protected abstract void AddItem(ResourceKey key, T item);
        
        protected abstract bool HasKey(ResourceKey key);
        
        public void Register(ResourceKey key, T item)
        {
            if (CurrentPhase == RegistryPhase.Open)
            {
                if (HasKey(key))
                {
                    throw new RegistryConflictException(key);
                }

                GemfruitMod.Logger.Log(LogLevel.Debug, GetType().Name,
                    $"Registering '{key}'");
                AddItem(key, item);
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Error, GetType().Name,
                    $"Attempted to register '{key}' before/after corresponding lifecycle event!");
            }
        }
    }
}