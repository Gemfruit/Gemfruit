using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Monsters;

// ReSharper disable MemberCanBePrivate.Global

namespace Gemfruit.Mod.Events.Monsters
{
    public class WildernessSpawnRegistrationEvent : PhasedEvent
    {
        public WildernessSpawnRegistry Registry { get; }
        public WildernessArea Area { get; }

        public WildernessSpawnRegistrationEvent(WildernessSpawnRegistry registry, EventPhase phase, WildernessArea area)
            : base(phase)
        {
            Registry = registry;
            Area = area;
        }
    }
}