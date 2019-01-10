using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public class BaseFieldModel
    {
        public virtual string Name { get; set; }
        public virtual BaseAccessModifier Access { get; set; }
        public virtual BaseReflectedType Type { get; set; }

    }
}
