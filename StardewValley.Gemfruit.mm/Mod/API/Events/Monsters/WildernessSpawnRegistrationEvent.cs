using Gemfruit.Mod.Monsters;

// ReSharper disable MemberCanBePrivate.Global

namespace Gemfruit.Mod.API.Events.Monsters
{
    public class WildernessSpawnRegistrationEvent : PhasedEvent
    {
        public WildernessSpawnRegistry Registry { get; }
        public MonsterLocomotion Type { get; }

        public WildernessSpawnRegistrationEvent(WildernessSpawnRegistry registry, EventPhase phase, MonsterLocomotion type)
            : base(phase)
        {
            Registry = registry;
            Type = type;
        }
    }
}