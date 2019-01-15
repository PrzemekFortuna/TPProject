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
    public class XMLMethodModel : BaseMethodModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public XMLReflectedType ReturnType { get; set; }
        [DataMember]
        public XMLAccessModifier Access { get; set; }
        [DataMember]
        public List<XMLParameterModel> Parameters { get; set; }
    }
}
