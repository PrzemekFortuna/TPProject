using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public class BaseReflectionModel
    {
        public string Name { get; set; }
        public List<BaseNamespaceModel> Namespaces { get; set; }
    }
}
