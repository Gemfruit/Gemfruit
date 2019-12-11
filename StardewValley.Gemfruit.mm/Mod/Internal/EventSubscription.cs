using System;
using Gemfruit.Mod.Events;

namespace Gemfruit.Mod.Internal
{
    public class EventSubscription<TEvent> : ISubscription 
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