using System;
using System.Collections.Generic;
using Gemfruit.Mod.API.Events;

namespace Gemfruit.Mod.Internal
{
    internal class EventBus
    {
        private readonly Dictionary<Type, List<ISubscription>> _subscriptions;

        public EventBus()
        {
            _subscriptions = new Dictionary<Type, List<ISubscription>>();
        }

        public void FireEvent<TEvent>(TEvent evt)
            where TEvent: EventBase
        {
            if (!_subscriptions.ContainsKey(typeof(TEvent))) return;
            foreach (var s in _subscriptions[typeof(TEvent)])
            {
                s.Invoke(evt);
            }
        }

        public void AddHandler<TEvent>(Action<TEvent> handler)
            where TEvent: EventBase
        {
            if (!_subscriptions.ContainsKey(typeof(TEvent)))
            {
                _subscriptions.Add(typeof(TEvent), new List<ISubscription>());
            }
            _subscriptions[typeof(TEvent)].Add(new EventSubscription<TEvent>(handler));
        }
    }
}