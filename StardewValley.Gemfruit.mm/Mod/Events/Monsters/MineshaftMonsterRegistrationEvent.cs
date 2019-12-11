using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Monsters;

// ReSharper disable MemberCanBePrivate.Global

namespace Gemfruit.Mod.Events.Monsters
{
    public class MineshaftMonsterRegistrationEvent : PhasedEvent
    {
        public MineshaftMonsterRegistry Registry { get; }

        public MineshaftMonsterRegistrationEvent(MineshaftMonsterRegistry registry, EventPhase phase)
            : base(phase)
        {
            Registry = registry;
        }
    }
}