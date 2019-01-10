using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public abstract class BasePropertyModel
    {
        public enum Access
        {
            ReadOnly,
            ReadWrite
        }

        public virtual string Name { get; set; }
        public virtual Access PropertyAccess { get; set; }
        public virtual BaseMethodModel SetMethod { get; set; }
        public virtual BaseMethodModel GetMethod { get; set; }
        public virtual BaseReflectedType Type { get; set; }
    }
}
