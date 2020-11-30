using Gemfruit.Mod.Placeables;

namespace Gemfruit.Mod.API.Events.Placeables
{
    public class PlaceableRegistrationEvent : PhasedEvent
    {
        public PlaceableRegistry Registry { get; }


        public PlaceableRegistrationEvent(PlaceableRegistry registry, EventPhase phase) : base(phase)
        {
            Registry = registry;
        }
    }
}