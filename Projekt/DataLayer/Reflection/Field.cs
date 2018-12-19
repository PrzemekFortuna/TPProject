using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TPProjectLib.Utility;

namespace TPProjectLib.Reflection
{    
    public class Field
    {
        public string Name { get;  set; }
        public AccessModifier Access { get;  set; }
        public ReflectedType Type { get;  set; }

        public Field()
        {

        }
        public Field(FieldInfo info)
        {
            Name = info.Name;
            Access = GetAccess(info);
            Type = GetType(info);
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

        private ReflectedType GetType(FieldInfo info)
        {
            if (!SingletonDictionary<ReflectedType>.Types.ContainsKey(info.FieldType.Name))
            {
                ReflectedType reflectedType = new ReflectedType(info.FieldType);
                if(!SingletonDictionary<ReflectedType>.Types.ContainsKey(reflectedType.Name))
                    SingletonDictionary<ReflectedType>.Types.Add(info.FieldType.Name, reflectedType);
            }

            return SingletonDictionary<ReflectedType>.Types[info.FieldType.Name];
        }
    }
}
