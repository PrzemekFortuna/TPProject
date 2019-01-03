using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serializers.XMLModel
{
    [DataContract(Namespace = "", IsReference = true)]
    public class XMLReflectedType : BaseReflectedType
    {
        [DataMember]
        public override string Name { get; set; }
        [DataMember]
        public override string Namespace { get; set; }
        [DataMember]
        public override bool IsStatic { get; set; }
        [DataMember]
        public override bool IsAbstract { get; set; }
        [DataMember]
        public new XMLKindModel TypeKind { get; set; }
        [DataMember]
        public new XMLAccessModifier Access { get; set; }
        [DataMember]
        public new XMLReflectedType BaseType { get; set; }

        public override List<Attribute> Attributes { get; set; }
        [DataMember]
        public new List<XMLMethodModel> Constructors { get; set; }
        [DataMember]
        public new List<XMLMethodModel> Methods { get; set; }
        [DataMember]
        public new List<XMLFieldModel> Fields { get; set; }
        [DataMember]
        public new List<XMLPropertyModel> Properties { get; set; }
        [DataMember]
        public new List<XMLReflectedType> ImplementedInterfaces { get; set; }
    }
}
