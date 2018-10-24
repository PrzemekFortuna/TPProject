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

        public Property(PropertyInfo info)
        {
            Name = info.Name;
            Type = Globals.Types[info.PropertyType.Name];
            PropertyAccess = (info.CanRead && (!info.CanWrite)) ? Access.ReadOnly : Access.ReadWrite;
            GetSetMethods(info);
        }

        private void GetSetMethods(PropertyInfo info)
        {
            if(info.SetMethod != null)
                SetMethod = new Method(info.SetMethod);
            if(info.GetMethod != null)
                GetMethod = new Method(info.GetMethod);
        }

        private ReflectedType GetType(PropertyInfo info)
        {
            if(!Globals.Types.ContainsKey(info.PropertyType.Name))
                Globals.Types.Add(info.PropertyType.Name, new ReflectedType(info.PropertyType.Name, info.PropertyType.Namespace));

            return Globals.Types[info.PropertyType.Name];
        }

    }
}
