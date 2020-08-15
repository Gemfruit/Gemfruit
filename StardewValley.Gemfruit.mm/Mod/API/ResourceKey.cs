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
        
        public string Namespace { get; }
        public string Key { get; }
        
        /// <summary>
        /// Constructs a new <c>ResourceKey</c> with the requisite namespace and key.
        /// </summary>
        /// <param name="ns">Namespace of the new <c>ResourceKey</c></param>
        /// <param name="key">Key of the new <c>ResourceKey</c></param>
        public ResourceKey(string ns, string key)
        {
            Namespace = ns;
            Key = key;
        }

        /// <summary>
        /// Internal method used for saving some typing when it comes to registering vanilla
        /// <c>ResourceKey</c>s. Not accessible outside of Gemfruit because ideally no-one should be
        /// registering a key with the vanilla namespace, and we don't want this implicit behaviour
        /// to leak outside of Gemfruit.
        /// </summary>
        /// <param name="key">Key of the new <c>ResourceKey</c></param>
        internal ResourceKey(string key) : this(StardewValleyNamespace, key)
        {
            
        }

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