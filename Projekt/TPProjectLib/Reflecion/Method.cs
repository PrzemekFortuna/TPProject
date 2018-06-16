using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Reflection
{
    [DataContract(Namespace ="")]
    public class Method
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public ReflectedType ReturnType { get; private set; }
        [DataMember]
        public AccessModifier Access { get; private set; }
        [DataMember]
        public List<Parameter> Parameters { get; private set; }

        public Method(ConstructorInfo info)
        {
            Parameters = new List<Parameter>();
            Name = info.Name;
            Access = GetAccess(info);
            ReturnType = null;


        }

        public Method(MethodInfo info, Dictionary<string, ReflectedType> dictionary)
        {
            Name = info.Name;
            Access = GetAccess(info);
            ReturnType = GetReflectedType(info, dictionary);
            Parameters = GetParameters(info, dictionary);
        }

        private AccessModifier GetAccess(MethodBase type)
        {
            if (type.IsPublic)
                return AccessModifier.Public;

            if (type.IsFamily)
                return AccessModifier.Protected;

            if (type.IsFamilyAndAssembly)
                return AccessModifier.ProtectedInternal;

            return AccessModifier.Private;
        }

        private ReflectedType GetReflectedType(MethodInfo info, Dictionary<string, ReflectedType> dictionary)
        {
            if (!dictionary.ContainsKey(info.ReturnType.Name))
                dictionary.Add(info.ReturnType.Name, new ReflectedType(info.ReturnType.Name, info.ReturnType.Namespace));
            return dictionary[info.ReturnType.Name];
        }

        private List<Parameter> GetParameters(MethodBase info, Dictionary<string, ReflectedType> dictionary)
        {
            List<Parameter> parameters =  (from ParameterInfo param in info.GetParameters() select new Parameter(param.Name, param.ParameterType)).ToList();
            foreach(Parameter param in parameters)
            {
                if (!dictionary.ContainsKey(param.ParamType.Name))
                    dictionary.Add(param.ParamType.Name, new ReflectedType(param.ParamType.Name, param.ParamType.Namespace));
            }

            return parameters;
        }
    }
}
