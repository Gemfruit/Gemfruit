using System;
using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Internal.Exceptions
{
    /// <summary>
    /// RegistryConflictException is an exception thrown when an <see cref="IRegistry{T}"/>
    /// receives a request to register a <see cref="ResourceKey"/> that the <see cref="IRegistry{T}"/>
    /// already contains.
    /// </summary>
    public class RegistryConflictException : Exception
    {
        public RegistryConflictException(ResourceKey conflicting) : base("Unable to reconcile two conflicting registry keys, both " + conflicting + "!")
        {
        }
    }
}