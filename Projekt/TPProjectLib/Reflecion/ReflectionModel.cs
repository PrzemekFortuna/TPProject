using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Reflection
{
    [DataContract(Namespace ="")]
    public class ReflectionModel
    {
        [DataMember]
        public List<Namespace> Namespaces { get; set; }
        public Dictionary<string, ReflectedType> TypesDictionary { get; set; }

        private Assembly _assembly;

        public ReflectionModel(string path)
        {
            Namespaces = new List<Namespace>();
            _assembly = Assembly.LoadFile(path);
            TypesDictionary = new Dictionary<string, ReflectedType>();
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
                ReflectedType reflectedType = new ReflectedType(type, TypesDictionary);
                Namespaces.Find(x => x.Name == type.Namespace).AddElement(reflectedType);
                if (!TypesDictionary.ContainsKey(reflectedType.Name))
                    TypesDictionary.Add(reflectedType.Name, new ReflectedType(reflectedType.Name, reflectedType.Namespace));
                TypesDictionary[type.Name].AnalyzeType(type, TypesDictionary);
            }
        }

    }
}
