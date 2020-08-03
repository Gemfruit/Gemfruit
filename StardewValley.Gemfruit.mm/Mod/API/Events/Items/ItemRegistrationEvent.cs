using Gemfruit.Mod.Items;

namespace Gemfruit.Mod.API.Events.Items
{
    public class ItemRegistrationEvent : PhasedEvent
    {
        public ItemRegistry Registry { get; }


        public ItemRegistrationEvent(ItemRegistry registry, EventPhase phase) : base(phase)
        {
            Registry = registry;
        }
    }
}