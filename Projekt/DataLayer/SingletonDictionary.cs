using System.Collections.Generic;

namespace BusinessLogic
{
    public sealed class SingletonDictionary<T>
    {
        public static SingletonDictionary<T> Types { get; set; } = new SingletonDictionary<T>();
        private Dictionary<string, T> _dictionary;

        private SingletonDictionary()
        {
            _dictionary = new Dictionary<string, T>();
        }

        public void Add(string key, T value)
        {
            _dictionary.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public T this[string name]
        {
            get
            {
                return _dictionary[name];
            }
        }
    }
}
