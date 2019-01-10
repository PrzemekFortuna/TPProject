using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public abstract class BaseMethodModel
    {
        public virtual string Name { get; set; }
        public virtual BaseReflectedType ReturnType { get; set; }
        public virtual BaseAccessModifier Access { get; set; }
        public virtual List<BaseParameterModel> Parameters { get; set; }
    }
}
