using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    public class BaseFieldModel
    {
        public virtual string Name { get; set; }
        public virtual BaseAccessModifier Access { get; set; }
        public virtual BaseReflectedType Type { get; set; }

    }
}
