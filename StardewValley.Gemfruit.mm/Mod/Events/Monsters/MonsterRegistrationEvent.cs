using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Monsters;

// ReSharper disable MemberCanBePrivate.Global

namespace Gemfruit.Mod.Events.Monsters
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