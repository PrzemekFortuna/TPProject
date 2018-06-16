using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Reflection
{
    [DataContract(Namespace ="")]
    public enum AccessModifier
    {
        [EnumMember]
        Public,
        [EnumMember]
        Private,
        [EnumMember]
        Protected,
        [EnumMember]
        ProtectedInternal
    }
}
