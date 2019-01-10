using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public abstract class BaseNamespaceModel
    {
        public virtual string Name { get; set; }
        public virtual List<BaseReflectedType> Classes { get; set; }
        public virtual List<BaseReflectedType> Interfaces { get; set; }
        public virtual List<BaseReflectedType> ValueTypes { get; set; }
    }
}
