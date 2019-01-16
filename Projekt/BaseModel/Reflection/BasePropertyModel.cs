using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Reflection
{
    [DataContract(Namespace = "")]
    public class BasePropertyModel
    {
        public enum Access
        {
            ReadOnly,
            ReadWrite
        }

        public string Name { get; set; }
        public Access PropertyAccess { get; set; }
        public BaseMethodModel SetMethod { get; set; }
        public BaseMethodModel GetMethod { get; set; }
        public BaseReflectedType Type { get; set; }
    }
}
