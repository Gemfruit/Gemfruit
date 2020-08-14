using System;

namespace Gemfruit.Mod.Internal
{
    internal static class BoolUtility
    {
        /// <summary>
        /// This method serves to parse any known form of vaguely 'truthy' or 'falsy' value within reason.
        /// This supplants <see cref="Boolean.Parse"/> due its general inadequacies in parsing anything but
        /// the literal strings <c>True</c> and <c>False</c>.
        /// </summary>
        /// <param name="pars">String to parse into a boolean</param>
        /// <returns><c>true</c> if the value is truthy, or <c>false</c> if the value is falsy.</returns>
        /// <exception cref="ArgumentException">Thrown if the value cannot be classified as either truthy or falsy.    </exception>
        public static bool Parse(string pars)
        {
            switch (pars.ToLower())
            {
                case "true":
                case "1":
                case "y":
                case "yes":
                    return true;
                case "false":
                case "0":
                case "n":
                case "no":
                    return false;
                default:
                    throw new ArgumentException($"Value '{pars}' couldn't be interpreted as truthy or falsy");
            }
        }
    }
}