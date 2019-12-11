using Gemfruit.Mod.Events;

namespace Gemfruit.Mod.Internal
{
    public interface ISubscription
    {
        void Invoke(EventBase eventBase);
    }
}