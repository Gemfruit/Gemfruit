using System;
using System.Collections.Generic;

namespace Gemfruit.Mod.Items.Container
{
    public interface IHasContainers
    { 
        bool HasContainer(Type containerType);
        T GetContainer<T>() where T: IContainer;
        bool TryFillContainer<T>(IReadOnlyDictionary<string, object> dict) where T : IContainer;
    }
}