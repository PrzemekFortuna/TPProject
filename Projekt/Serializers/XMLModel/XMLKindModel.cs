using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serializers.XMLModel
{
    [DataContract(Namespace = "")]
    public enum XMLKindModel
    {
        [EnumMember]
        Class,
        [EnumMember]
        Interface,
        [EnumMember]
        Struct,
        [EnumMember]
        Enum
    }
}
