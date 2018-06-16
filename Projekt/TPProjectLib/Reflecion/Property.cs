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
    public class Property
    {
        [DataContract(Namespace ="")]
        public enum Access
        {
            [EnumMember]
            ReadOnly,
            [EnumMember]
            ReadWrite
        }

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public Access PropertyAccess { get; private set; }
        [DataMember]
        public Method SetMethod { get; private set; }
        [DataMember]
        public Method GetMethod { get; private set; }
        [DataMember]
        public ReflectedType Type { get; private set; }

        public Property(PropertyInfo info, Dictionary<string, ReflectedType> dictionary)
        {
            Name = info.Name;
            Type = dictionary[info.PropertyType.Name];
            PropertyAccess = (info.CanRead && (!info.CanWrite)) ? Access.ReadOnly : Access.ReadWrite;
            GetSetMethods(info, dictionary);
        }

        private void GetSetMethods(PropertyInfo info, Dictionary<string, ReflectedType> dictionary)
        {
            if(info.SetMethod != null)
                SetMethod = new Method(info.SetMethod, dictionary);
            if(info.GetMethod != null)
                GetMethod = new Method(info.GetMethod, dictionary);
        }

        private ReflectedType GetType(PropertyInfo info, Dictionary<string, ReflectedType> dictionary)
        {
            if(!dictionary.ContainsKey(info.PropertyType.Name))
                dictionary.Add(info.PropertyType.Name, new ReflectedType(info.PropertyType.Name, info.PropertyType.Namespace));

            return dictionary[info.PropertyType.Name];
        }

    }
}
