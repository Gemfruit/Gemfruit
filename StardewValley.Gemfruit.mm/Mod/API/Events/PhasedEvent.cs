namespace Gemfruit.Mod.API.Events
{
    public class PhasedEvent : EventBase
    {
        public EventPhase Phase { get; }

        public PhasedEvent(EventPhase phase)
        {
            Phase = phase;
        }
    }
}