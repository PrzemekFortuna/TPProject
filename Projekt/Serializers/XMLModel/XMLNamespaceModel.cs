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
    public class XMLNamespaceModel : BaseNamespaceModel
    {
        [DataMember]
        public override string Name { get; set; }
        [DataMember]
        public new List<XMLReflectedType> Classes { get; set; }
        [DataMember]
        public new List<XMLReflectedType> Interfaces { get; set; }
        [DataMember]
        public new List<XMLReflectedType> ValueTypes { get; set; }
    }
}
