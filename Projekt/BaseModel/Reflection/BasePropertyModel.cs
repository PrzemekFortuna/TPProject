using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
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
        public virtual BaseMethodModel GetModel { get; set; }
        public virtual BaseReflectedType Type { get; set; }
    }
}
