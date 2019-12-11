namespace Gemfruit.Mod.Internal
{
    public class RegistryKey
    {
        public RegistryKey(string ns, string key)
        {
            Namespace = ns;
            Key = key;
        }

        public RegistryKey(string key) : this("stardew_valley", key)
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
            if (obj is RegistryKey)
            {
                var other = (RegistryKey) obj;
                return other.Namespace == this.Namespace && other.Key == this.Key;
            }
            return false;
        }
    }
}