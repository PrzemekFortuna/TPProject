using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataLayer.Reflection
{   
    public class ReflectionModel
    {
        public List<Namespace> Namespaces { get; set; }
        //public Dictionary<string, ReflectedType> TypesDictionary { get; set; }

        private Assembly _assembly;

        public ReflectionModel()
        {
        }

        public ReflectionModel(string path)
        {
            Namespaces = new List<Namespace>();
            _assembly = Assembly.LoadFile(path);
            Reflect();
        }

        public void Reflect()
        {
            //load namespaces
            foreach (string ns in _assembly.GetTypes().Select(x => x.Namespace).Distinct())
            {
                Namespaces.Add(new Namespace(ns));
            }

            //add types to namespaces
            foreach(Type type in _assembly.GetTypes())
            {
                ReflectedType reflectedType = new ReflectedType(type);
                Namespaces.Find(x => x.Name == type.Namespace).AddElement(reflectedType);
                if (!SingletonDictionary<ReflectedType>.Types.ContainsKey(reflectedType.Name))
                    SingletonDictionary<ReflectedType>.Types.Add(reflectedType.Name, new ReflectedType(reflectedType.Name, reflectedType.Namespace));
                SingletonDictionary<ReflectedType>.Types[type.Name].AnalyzeType(type);
            }
        }

    }
}
