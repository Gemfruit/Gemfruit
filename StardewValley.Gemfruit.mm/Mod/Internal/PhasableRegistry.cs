namespace Gemfruit.Mod.Internal
{
    public abstract class PhasableRegistry
    {
        protected RegistryPhase CurrentPhase { get; private set; } = RegistryPhase.Closed;
        
        public void Initialize()
        {
            CurrentPhase = RegistryPhase.Open;
            InitializeRecords();
            CurrentPhase = RegistryPhase.Frozen;
        }

        protected abstract void InitializeRecords();
    }
}