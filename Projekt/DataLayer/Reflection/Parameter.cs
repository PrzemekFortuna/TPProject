using System;

namespace DataLayer.Reflection
{    
    public class Parameter
    {
        public string Name { get;  set; }
        public ReflectedType ParamType { get;  set; }

        public Parameter()
        {

        }
        public Parameter(string name, Type paramType)
        {
            Name = name;
            ParamType = new ReflectedType(paramType.Name, paramType.Namespace);
        }
    }
}
