namespace Gemfruit.Mod.Internal
{
    internal class StringUtility
    {
        internal static string SanitizeName(string name)
        {
            return name.ToLower().Replace(' ', '_')
                .Replace('-', '_')
                .Replace("l.", "large")
                .Replace(":", "")
                .Replace("'", "");
        }
    }
}