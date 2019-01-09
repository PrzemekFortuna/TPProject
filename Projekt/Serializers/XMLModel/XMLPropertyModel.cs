using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serializers.XMLModel
{
    [DataContract(Namespace = "")]
    public class XMLPropertyModel : BasePropertyModel
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
        public override string Name { get; set; }
        [DataMember]
        public new Access PropertyAccess { get; set; }
        [DataMember]
        public new XMLMethodModel SetMethod { get; set; }
        [DataMember]
        public new XMLMethodModel GetMethod { get; set; }
        [DataMember]
        public new XMLReflectedType Type { get; set; }
    }
}
