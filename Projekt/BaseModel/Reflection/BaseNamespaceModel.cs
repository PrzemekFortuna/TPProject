using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public class BaseNamespaceModel
    {
        public string Name { get; set; }
        public List<BaseReflectedType> Classes { get; set; }
        public List<BaseReflectedType> Interfaces { get; set; }
        public List<BaseReflectedType> ValueTypes { get; set; }
    }
}
