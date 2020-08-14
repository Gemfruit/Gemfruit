using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Internal
{
    /// <summary>
    /// Internal static class for various utilities relating to string manipulation, usually with the
    /// aim of simplifying conversion of properties from vanilla datafiles to the new Gemfruit API.
    /// </summary>
    internal static class StringUtility
    {
        /// <summary>
        /// Purely internal function to sanitize the internal Stardew Valley names of items/placeables.
        /// These names have to be turned into registry keys on the fly, so as a result we rely upon
        /// this odd, janky function to take these names and make them behave according to a simple,
        /// reproducible format without extraneous punctuation or needless abbreviation.
        /// </summary>
        /// <param name="name">Original name of an item/placeable</param>
        /// <returns>Sanitized name, for use in a <see cref="ResourceKey"/></returns>
        internal static string SanitizeName(string name)
        {
            return name.ToLower()
                .Replace("l.", "large")
                .Replace("lg.", "large")
                .Replace("s.", "small")
                .Replace("j.", "joja")
                .Replace("o'", "of")
                .Replace("?", "")
                .Replace("#", "no_")
                .Replace(":", "")
                .Replace("'", "")
                .Replace('.', '_')
                .Replace('-', '_')
                .Replace(' ', '_');
        }
    }
}