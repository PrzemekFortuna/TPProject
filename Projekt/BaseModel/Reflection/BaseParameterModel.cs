using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    public abstract class BaseParameterModel
    {
        public virtual string Name { get; set; }
        public virtual BaseReflectedType ParamType { get; set; }
    }
}
