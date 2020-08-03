namespace Gemfruit.Mod.API.Utility.Registry
{
    public abstract class PhasableRegistry
    {
        internal RegistryPhase CurrentPhase { get; private set; } = RegistryPhase.Closed;
        
        public void Initialize()
        {
            CurrentPhase = RegistryPhase.Open;
            InitializeRecords();
            CurrentPhase = RegistryPhase.Frozen;
        }

        protected abstract void InitializeRecords();
    }
}