using Gemfruit.Mod.Internal;

namespace Gemfruit.Mod.API.Utility.Registry
{
    public abstract class PhasableRegistry
    {
        internal RegistryPhase CurrentPhase { get; private set; } = RegistryPhase.Closed;
        
        public void Initialize()
        {
            GemfruitMod.Logger.Log(LogLevel.Info, GetType().Name, $"Initializing {GetType().Name}");
            CurrentPhase = RegistryPhase.Open;
            InitializeRecords();
            CurrentPhase = RegistryPhase.Frozen;
        }

        protected abstract void InitializeRecords();
    }
}