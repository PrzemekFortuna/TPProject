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
    public class XMLMethodModel : BaseMethodModel
    {
        [DataMember]
        public override string Name { get; set; }
        [DataMember]
        public new XMLReflectedType ReturnType { get; set; }
        [DataMember]
        public new XMLAccessModifier Access { get; set; }
        [DataMember]
        public new List<XMLParameterModel> Parameters { get; set; }
    }
}
