using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public class BaseMethodModel
    {
        public string Name { get; set; }
        public BaseReflectedType ReturnType { get; set; }
        public BaseAccessModifier Access { get; set; }
        public List<BaseParameterModel> Parameters { get; set; }
    }
}
