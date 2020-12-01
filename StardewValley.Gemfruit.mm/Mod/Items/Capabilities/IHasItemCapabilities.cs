using System;
using Gemfruit.Mod.API.Utility;

namespace Gemfruit.Mod.Items.Capabilities
{
    public interface IHasItemCapabilities
    {
        Result<TCap, Exception> GetCapability<TCap>() where TCap: ItemCapability;
        bool HasCapability<TCap>() where TCap: ItemCapability;
        bool HasCapability(Type capType);
        void AddCapability(ItemCapability capability);
    }
}