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
    [DataContract]
    public class Field
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public AccessModifier Access { get; private set; }
        [DataMember]
        public ReflectedType Type { get; private set; }

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
            if (!Globals.Types.ContainsKey(info.FieldType.Name))
            {
                ReflectedType reflectedType = new ReflectedType(info.FieldType);
                if(!Globals.Types.ContainsKey(reflectedType.Name))
                    Globals.Types.Add(info.FieldType.Name, reflectedType);
            }

            return Globals.Types[info.FieldType.Name];
        }
    }
}
