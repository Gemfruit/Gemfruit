namespace Gemfruit.Mod.API
{
    /// <summary>
    /// <c>ResourceKey</c> is a class describing namespaced entries into an arbitrary map. Typically,
    /// this is to avoid resource name conflicts by adding one layer of differentiability, usually defined
    /// as the name of the originating assembly of the name being registered, but not necessarily
    /// restricted to this.
    /// </summary>
    public class ResourceKey
    {
        public const string StardewValleyNamespace = "stardew_valley";
        
        public ResourceKey(string ns, string key)
        {
            Namespace = ns;
            Key = key;
        }

        internal ResourceKey(string key) : this(StardewValleyNamespace, key)
        {
            
        }

        public string Namespace { get; }
        public string Key { get; }

        public override string ToString()
        {
            return $"{Namespace}:{Key}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ResourceKey other)
            {
                return other.Namespace == Namespace && other.Key == Key;
            }
            return false;
        }
    }
}