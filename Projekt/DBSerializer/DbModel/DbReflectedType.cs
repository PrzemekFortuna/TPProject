using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbReflectedType : BaseReflectedType
    {
        public int DbReflectedTypeId { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }
        //public BaseKindModel TypeKind { get; set; }
        //public BaseAccessModifier Access { get; set; }
        public DbReflectedType BaseType { get; set; }
        public List<DbReflectedType> Attributes { get; set; }
        public List<DbMethodModel> Constructors { get; set; }
        public List<DbMethodModel> Methods { get; set; }  
        public List<DbFieldModel> Fields { get; set; }
        public List<DbPropertyModel> Properties { get; set; }
        public List<DbReflectedType> ImplementedInterfaces { get; set; }
    }
}
