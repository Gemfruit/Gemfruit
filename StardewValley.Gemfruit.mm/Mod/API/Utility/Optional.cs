namespace Gemfruit.Mod.API.Utility
{
    public class Optional<T>
    {
        private readonly T _value;
        private readonly bool _present;
        
        public Optional(T value)
        {
            _value = value;
            _present = value != null;
        }

        private Optional()
        {
            _present = false;
        }

        /// <summary>
        /// Force-unwrap the Optional.
        /// WARNING: This will and can return `null` if the optional type is empty!
        /// </summary>
        /// <returns>The internal value, without any explicit checks.</returns>
        public T Unwrap()
        {
            return _value;
        }

        /// <summary>
        /// Check if a value is present in the optional.
        /// </summary>
        /// <returns>Whether or not the value is present within the optional.</returns>
        public bool IsPresent()
        {
            return _present;
        }

        public static Optional<T> None()
        {
            return new Optional<T>();
        }
    }
}