﻿using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serializers.XMLModel
{
    [DataContract(Namespace = "")]
    public class XMLParameterModel : BaseParameterModel
    {
        [DataMember]
        public override string Name { get; set; }
        [DataMember]
        public new XMLReflectedType ParamType { get; set; }
    }
}
