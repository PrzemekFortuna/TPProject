using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TPProjectLib.Utility;

namespace TPProjectLib.Reflection
{
    public class Method
    {
        public string Name { get; private set; }
        public ReflectedType ReturnType { get; private set; }       
        public AccessModifier Access { get; private set; }        
        public List<Parameter> Parameters { get; private set; }

        public Method(ConstructorInfo info)
        {
            Parameters = new List<Parameter>();
            Name = info.Name;
            Access = GetAccess(info);
            ReturnType = null;


        }

        public Method(MethodInfo info)
        {
            Name = info.Name;
            Access = GetAccess(info);
            ReturnType = GetReflectedType(info);
            Parameters = GetParameters(info);
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

        private ReflectedType GetReflectedType(MethodInfo info)
        {
            if (!SingletonDictionary<ReflectedType>.Types.ContainsKey(info.ReturnType.Name))
                SingletonDictionary<ReflectedType>.Types.Add(info.ReturnType.Name, new ReflectedType(info.ReturnType.Name, info.ReturnType.Namespace));
            return SingletonDictionary<ReflectedType>.Types[info.ReturnType.Name];
        }

        private List<Parameter> GetParameters(MethodBase info)
        {
            List<Parameter> parameters =  (from ParameterInfo param in info.GetParameters() select new Parameter(param.Name, param.ParameterType)).ToList();
            foreach(Parameter param in parameters)
            {
                if (!SingletonDictionary<ReflectedType>.Types.ContainsKey(param.ParamType.Name))
                    SingletonDictionary<ReflectedType>.Types.Add(param.ParamType.Name, new ReflectedType(param.ParamType.Name, param.ParamType.Namespace));
            }

            return parameters;
        }
    }
}
