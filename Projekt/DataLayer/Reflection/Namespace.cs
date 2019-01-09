using System.Collections.Generic;

namespace BusinessLogic.Reflection
{
    public class Namespace
    {
        public string Name { get; private set; }        
        public List<ReflectedType> Classes { get; set; }
        public List<ReflectedType> Interfaces { get; set; }
        public List<ReflectedType> ValueTypes { get; set; }

        public Namespace(string name)
        {
            Name = name;
            Classes = new List<ReflectedType>();
            Interfaces = new List<ReflectedType>();
            ValueTypes = new List<ReflectedType>();
        }

        public void AddElement(ReflectedType type)
        {
            if(type != null)
            {
                if (type.TypeKind == Kind.Class)
                    Classes.Add(type);
                else if (type.TypeKind == Kind.Interface)
                    Interfaces.Add(type);
                else if (type.TypeKind == Kind.Enum || type.TypeKind == Kind.Struct)
                    ValueTypes.Add(type);
            }
        }

    }
}
