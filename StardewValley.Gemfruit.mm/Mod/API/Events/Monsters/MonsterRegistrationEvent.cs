using Gemfruit.Mod.Monsters;

// ReSharper disable MemberCanBePrivate.Global

namespace Gemfruit.Mod.API.Events.Monsters
{
    public class MonsterRegistrationEvent : PhasedEvent
    {
        public MonsterRegistry Registry { get; }

        public MonsterRegistrationEvent(MonsterRegistry registry, EventPhase phase)
            : base(phase)
        {
            Registry = registry;
        }
    }
}