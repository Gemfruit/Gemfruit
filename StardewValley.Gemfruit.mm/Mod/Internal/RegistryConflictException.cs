using System;

namespace Gemfruit.Mod.Internal
{
    /// <summary>
    /// RegistryConflictException is an exception thrown when an <see cref="IRegistry{T}"/>
    /// receives a request to register a <see cref="RegistryKey"/> that the <see cref="IRegistry{T}"/>
    /// already contains.
    /// </summary>
    public class RegistryConflictException : Exception
    {
        public RegistryConflictException(RegistryKey conflicting) : base("Unable to reconcile two conflicting registry keys, both " + conflicting + "!")
        {
        }
    }
}