﻿using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serializers.XMLModel
{
    [DataContract(IsReference =true)]
    public class XMLFieldModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public XMLAccessModifier Access { get; set; }
        [DataMember]
        public XMLReflectedType Type { get; set; }
    }
}
