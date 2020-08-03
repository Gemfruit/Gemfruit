using Gemfruit.Mod.Monsters;

// ReSharper disable MemberCanBePrivate.Global

namespace Gemfruit.Mod.API.Events.Monsters
{
    public class MineshaftSpawnRegistrationEvent : PhasedEvent
    {
        public MineshaftSpawnRegistry Registry { get; }
        public MonsterLocomotion Type { get; }
        public MineshaftArea Area { get; }
        

        public MineshaftSpawnRegistrationEvent(MineshaftSpawnRegistry registry, EventPhase phase, MonsterLocomotion type, MineshaftArea area)
            : base(phase)
        {
            Registry = registry;
            Type = type;
            Area = area;
        }
    }
}