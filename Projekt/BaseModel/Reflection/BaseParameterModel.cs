using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public class BaseParameterModel
    {
        public string Name { get; set; }
        public BaseReflectedType ParamType { get; set; }
    }
}
