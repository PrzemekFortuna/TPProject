using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serializers.XMLModel
{
    [DataContract(Namespace = "")]
    public class XMLReflectedType : BaseReflectedType
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Namespace { get; set; }
        [DataMember]
        public bool IsStatic { get; set; }
        [DataMember]
        public bool IsAbstract { get; set; }
        [DataMember]
        public XMLKindModel TypeKind { get; set; }
        [DataMember]
        public XMLAccessModifier Access { get; set; }
        [DataMember]
        public XMLReflectedType BaseType { get; set; }

        public List<Attribute> Attributes { get; set; }
        [DataMember]
        public List<XMLMethodModel> Constructors { get; set; }
        [DataMember]
        public List<XMLMethodModel> Methods { get; set; }
        [DataMember]
        public List<XMLFieldModel> Fields { get; set; }
        [DataMember]
        public List<XMLPropertyModel> Properties { get; set; }
        [DataMember]
        public List<XMLReflectedType> ImplementedInterfaces { get; set; }
    }
}
