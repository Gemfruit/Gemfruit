using Gemfruit.Mod.Resources;

namespace Gemfruit.Mod.API.Events.Resources
{
    public class ResourceLoadEvent : PhasedEvent
    {
        public ResourceRegistry Registry { get; }
        
        public ResourceLoadEvent(ResourceRegistry registry, EventPhase phase) : base(phase)
        {
            Registry = registry;
        }
    }
}