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
    public class XMLReflectionModel : BaseReflectionModel
    {
        [DataMember]
        public new List<XMLNamespaceModel> Namespaces { get; set; }
    }
}
