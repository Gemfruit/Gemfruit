using Gemfruit.Mod.Items;

namespace Gemfruit.Mod.API.Events.Items
{
    public class ArtifactDropRegistrationEvent : PhasedEvent
    {
        public ArtifactDropRegistry Registry { get; }

        public ArtifactDropRegistrationEvent(ArtifactDropRegistry registry, EventPhase phase) : base(phase)
        {
            Registry = registry;
        }
    }
}