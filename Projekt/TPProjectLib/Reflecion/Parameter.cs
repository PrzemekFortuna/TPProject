using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Reflection
{
    [DataContract(Namespace ="")]
    public class Parameter
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public ReflectedType ParamType { get; private set; }

        public Parameter(string name, Type paramType)
        {
            Name = name;
            ParamType = new ReflectedType(paramType.Name, paramType.Namespace);
        }
    }
}
