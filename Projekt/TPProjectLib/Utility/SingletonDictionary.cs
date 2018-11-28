using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPProjectLib.Reflection;

namespace TPProjectLib.Utility
{
    public sealed class SingletonDictionary
    {
        public static SingletonDictionary Types { get; set; } = new SingletonDictionary();
        private Dictionary<string, ReflectedType> _dictionary;

        private SingletonDictionary()
        {
            _dictionary = new Dictionary<string, ReflectedType>();
        }

        public void Add(string key, ReflectedType value)
        {
            _dictionary.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public ReflectedType this[string name]
        {
            get
            {
                return _dictionary[name];
            }
        }
    }
}
