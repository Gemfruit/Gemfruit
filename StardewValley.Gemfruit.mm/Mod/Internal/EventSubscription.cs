using System;
using Gemfruit.Mod.API.Events;

namespace Gemfruit.Mod.Internal
{
    internal class EventSubscription<TEvent> : ISubscription 
        where TEvent: EventBase
    {
        public Action<TEvent> Action;

        public EventSubscription(Action<TEvent> action)
        {
            Action = action;
        }

        public void Invoke(EventBase evt)
        {
            Action.Invoke((TEvent)evt);
        }
    }
}