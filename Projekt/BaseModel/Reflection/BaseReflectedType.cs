using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    public abstract class BaseReflectedType
    {
        public virtual string Name { get; set; }
        public virtual string Namespace { get; set; }
        public virtual bool IsStatic { get; set; }
        public virtual bool IsAbstract { get; set; }
        public virtual BaseKindModel TypeKind { get; set; }
        public virtual BaseAccessModifier Access { get; set; }
        public virtual BaseReflectedType BaseType { get; set; }
        public virtual List<Attribute> Attributes { get; set; }
        public virtual List<BaseMethodModel> Constructors { get; set; }
        public virtual List<BaseMethodModel> Methods { get; set; }
        public virtual List<BaseFieldModel> Fields { get; set; }
        public virtual List<BasePropertyModel> Properties { get; set; }
        public virtual List<BaseReflectedType> ImplementedInterfaces { get; set; }
    }
}
