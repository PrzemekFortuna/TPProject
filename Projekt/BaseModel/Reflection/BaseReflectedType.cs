using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public class BaseReflectedType
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }
        public BaseKindModel TypeKind { get; set; }
        public BaseAccessModifier Access { get; set; }
        public BaseReflectedType BaseType { get; set; }
        public List<BaseReflectedType> Attributes { get; set; }
        public List<BaseMethodModel> Constructors { get; set; }
        public List<BaseMethodModel> Methods { get; set; }
        public List<BaseFieldModel> Fields { get; set; }
        public List<BasePropertyModel> Properties { get; set; }
        public List<BaseReflectedType> ImplementedInterfaces { get; set; }
    }
}
