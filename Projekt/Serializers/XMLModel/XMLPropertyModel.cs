using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serializers.XMLModel
{
    [DataContract(Namespace = "", IsReference = true)]
    public class XMLPropertyModel
    {
        [DataContract(Namespace = "")]
        public enum Access
        {
            [EnumMember]
            ReadOnly,
            [EnumMember]
            ReadWrite
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Access PropertyAccess { get; set; }
        [DataMember]
        public XMLMethodModel SetMethod { get; set; }
        [DataMember]
        public XMLMethodModel GetMethod { get; set; }
        [DataMember]
        public XMLReflectedType Type { get; set; }
    }
}
