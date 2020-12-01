using System;
using System.Collections.Generic;
using Gemfruit.Mod.API.Utility;

namespace Gemfruit.Mod.Items.Container
{
    public interface IHasContainers
    { 
        Result<TCon, Exception> GetContainer<TCon>() where TCon: IContainer;
        bool HasContainer<TCon>() where TCon: IContainer;
        bool HasContainer(Type containerType);
        bool TryFillContainer<TCon>(IReadOnlyDictionary<string, object> dict) where TCon : IContainer;
    }
}