using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Monsters;
using StardewValley;

// ReSharper disable MemberCanBePrivate.Global

namespace Gemfruit.Mod.Events.Monsters
{
    public class WildernessMonsterRegistrationEvent : PhasedEvent
    {
        public WildernessMonsterRegistry Registry { get; }
        
        public WildernessMonsterRegistrationEvent(WildernessMonsterRegistry registry, EventPhase phase)
            : base(phase)
        {
            Registry = registry;
        }
    }
}