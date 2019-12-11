using System;

namespace Gemfruit.Mod.API
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class Module : Attribute
    {
        public string Id { get; }

        public Module(string id)
        {
            Id = id;
        }
    }
}