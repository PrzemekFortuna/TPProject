using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Reflection
{
    [DataContract]
    public class Field
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public AccessModifier Access { get; private set; }
        [DataMember]
        public ReflectedType Type { get; private set; }

        public Field(FieldInfo info, Dictionary<string, ReflectedType> dictionary)
        {
            Name = info.Name;
            Access = GetAccess(info);
            Type = GetType(info, dictionary);
        }

        private AccessModifier GetAccess(FieldInfo type)
        {
            if (type.IsPublic)
                return AccessModifier.Public;

            if (type.IsFamily)
                return AccessModifier.Protected;

            if (type.IsFamilyAndAssembly)
                return AccessModifier.ProtectedInternal;

            return AccessModifier.Private;
        }

        private ReflectedType GetType(FieldInfo info, Dictionary<string, ReflectedType> dictionary)
        {
            if (!dictionary.ContainsKey(info.FieldType.Name))
            {
                ReflectedType reflectedType = new ReflectedType(info.FieldType, dictionary);
                if(!dictionary.ContainsKey(reflectedType.Name))
                    dictionary.Add(info.FieldType.Name, reflectedType);
            }

            return dictionary[info.FieldType.Name];
        }
    }
}
