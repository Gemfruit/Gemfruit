using Gemfruit.Mod.API.Events;

namespace Gemfruit.Mod.Internal
{
    internal interface ISubscription
    {
        void Invoke(EventBase eventBase);
    }
}