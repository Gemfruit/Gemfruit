using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Monsters;

// ReSharper disable MemberCanBePrivate.Global

namespace Gemfruit.Mod.Events.Monsters
{
    public class MineshaftSpawnRegistrationEvent : PhasedEvent
    {
        public MineshaftSpawnRegistry Registry { get; }
        public MineshaftArea Area { get; }

        public MineshaftSpawnRegistrationEvent(MineshaftSpawnRegistry registry, EventPhase phase, MineshaftArea area)
            : base(phase)
        {
            Registry = registry;
            Area = area;
        }
    }
}