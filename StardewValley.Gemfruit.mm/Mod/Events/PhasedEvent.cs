using Gemfruit.Mod.Internal;

namespace Gemfruit.Mod.Events
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