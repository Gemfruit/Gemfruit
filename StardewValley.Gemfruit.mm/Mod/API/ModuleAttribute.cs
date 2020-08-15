using System;

namespace Gemfruit.Mod.API
{
    /// <summary>
    /// Modules are the main operative classes for the registration of mod code.
    /// Classes annotated with <c>Module</c> will be searched out by Gemfruit upon
    /// assembly loading, and will be added as mods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class Module : Attribute
    {
        /// <summary>
        /// The ID of the mod.
        /// </summary>
        /// <remarks>
        /// Note that this should be unique! Conflicts will cause errors, and will
        /// stop the game from starting.
        /// </remarks>
        public string Id { get; }

        public Module(string id)
        {
            Id = id;
        }
    }
}