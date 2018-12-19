using Serializers.XMLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPProjectLib.Reflection;

namespace Serializers
{
    public static class XMLMapper
    {
        public static XMLReflectionModel Map(ReflectionModel higherLayerModel)
        {
            return new XMLReflectionModel
            {
                Namespaces = higherLayerModel.Namespaces.Select(t => MapNamespace(t)).ToList()
            };
        }
        private static XMLNamespaceModel MapNamespace(Namespace ns)
        {
            return new XMLNamespaceModel
            {
                Classes = ns.Classes.Select(t => MapReflectedType(t)).ToList(),
                Interfaces = ns.Interfaces.Select(t => MapReflectedType(t)).ToList(),
                ValueTypes = ns.ValueTypes.Select(t => MapReflectedType(t)).ToList(),
                Name = ns.Name
            };
        }

        private static XMLReflectedType MapReflectedType(ReflectedType t)
        {
            return new XMLReflectedType
            {
                Access = (XMLAccessModifier)t.Access,
                Attributes = t.Attributes,
                BaseType = MapReflectedType(t.BaseType),
                Constructors = t.Constructors.Select(c => MapMethod(c)).ToList(),
                IsAbstract = t.IsAbstract,
                Fields = t.Fields.Select(f => MapField(f)).ToList(),
                ImplementedInterfaces = t.ImplementedInterfaces.Select(i => MapReflectedType(i)).ToList(),
                IsStatic = t.IsStatic,
                Methods = t.Methods.Select(m => MapMethod(m)).ToList(),
                Name = t.Name,
                Namespace = t.Namespace,
                Properties = t.Properties.Select(p => MapProperty(p)).ToList(),
                TypeKind = (XMLKindModel) t.TypeKind
            };
            
        }

        private static XMLMethodModel MapMethod(Method m)
        {
            return new XMLMethodModel
            {
                Access = (XMLAccessModifier)m.Access,
                Name = m.Name,
                Parameters = m.Parameters.Select(p => MapParameter(p)).ToList(),
                ReturnType = MapReflectedType(m.ReturnType)
            };
        }

        private static XMLFieldModel MapField(Field f)
        {
            return new XMLFieldModel
            {
                Access = (XMLAccessModifier)f.Access,
                Name = f.Name,
                Type = MapReflectedType(f.Type)
            };
        }

        private static XMLPropertyModel MapProperty(Property p)
        {
            return new XMLPropertyModel
            {
                PropertyAccess = (XMLPropertyModel.Access)p.PropertyAccess,
                GetMethod = MapMethod(p.GetMethod),
                SetMethod = MapMethod(p.SetMethod),
                Name = p.Name,
                Type = MapReflectedType(p.Type),
            };
        }

        private static XMLParameterModel MapParameter(Parameter p)
        {
            return new XMLParameterModel
            {
                Name = p.Name,
                ParamType = MapReflectedType(p.ParamType)
            };
        }
    }
}
