using DataLayer.Reflection;
using Serializers.XMLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Serializers
{
    public static class XMLMapper
    {
        private static Dictionary<string, XMLReflectedType> _xmlDictionary = new Dictionary<string, XMLReflectedType>();
        private static Dictionary<string, ReflectedType> _modelDictionary = new Dictionary<string, ReflectedType>();

        public static XMLReflectionModel MapToXMLModel(ReflectionModel higherLayerModel)
        {
            return new XMLReflectionModel
            {
                Namespaces = higherLayerModel.Namespaces.Select(t => MapNamespace(t)).ToList()
            };
        }

        public static ReflectionModel MapFromXMLModel(XMLReflectionModel higherLayerModel)
        {
            return new ReflectionModel
            {
                Namespaces = higherLayerModel.Namespaces.Select(t => MapNamespaceFromXML(t)).ToList()
            };
        }

        private static XMLNamespaceModel MapNamespace(Namespace ns)
        {
            return (ns == null) ? null : new XMLNamespaceModel
            {
                Classes = ns.Classes?.Select(t => MapReflectedType(t))?.ToList(),
                Interfaces = ns?.Interfaces.Select(t => MapReflectedType(t))?.ToList(),
                ValueTypes = ns?.ValueTypes.Select(t => MapReflectedType(t))?.ToList(),
                Name = ns.Name
            };
        }

        private static XMLReflectedType MapReflectedType(ReflectedType t)
        {
            if (t == null)
            {
                return null;
            }

            if (!_xmlDictionary.ContainsKey(t.Name))
            {
                _xmlDictionary.Add(t.Name, new XMLReflectedType { Name = t.Name, Namespace = t.Namespace });

                XMLReflectedType type = new XMLReflectedType
                {
                    Access = (XMLAccessModifier)t.Access,
                    Attributes = t.Attributes,
                    BaseType = MapReflectedType(t.BaseType),
                    Constructors = t.Constructors?.Select(c => MapMethod(c))?.ToList(),
                    IsAbstract = t.IsAbstract,
                    Fields = t.Fields?.Select(f => MapField(f))?.ToList(),
                    ImplementedInterfaces = t.ImplementedInterfaces?.Select(i => MapReflectedType(i))?.ToList(),
                    IsStatic = t.IsStatic,
                    Methods = t.Methods?.Select(m => MapMethod(m))?.ToList(),
                    Name = t.Name,
                    Namespace = t.Namespace,
                    Properties = t.Properties?.Select(p => MapProperty(p))?.ToList(),
                    TypeKind = (XMLKindModel)t.TypeKind
                };
                return type;
            }
            else
                return _xmlDictionary[t.Name];
        }

        private static XMLMethodModel MapMethod(Method m)
        {
            return (m == null) ? null : new XMLMethodModel
            {
                Access = (XMLAccessModifier)m.Access,
                Name = m.Name,
                Parameters = m.Parameters?.Select(p => MapParameter(p))?.ToList(),
                ReturnType = MapReflectedType(m.ReturnType)
            };
        }

        private static XMLFieldModel MapField(Field f)
        {
            return (f == null) ? null : new XMLFieldModel
            {
                Access = (XMLAccessModifier)f.Access,
                Name = f.Name,
                Type = MapReflectedType(f.Type)
            };
        }

        private static XMLPropertyModel MapProperty(Property p)
        {
            return (p == null) ? null : new XMLPropertyModel
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
            return (p == null) ? null : new XMLParameterModel
            {
                Name = p.Name,
                ParamType = MapReflectedType(p.ParamType)
            };
        }

        private static Namespace MapNamespaceFromXML(XMLNamespaceModel ns)
        {
            if (ns == null) return null;
            Namespace aNamespace = new Namespace(ns.Name);

            ns.Classes?.Select(t => MapReflectedTypeFromXML(t))?.ToList().ForEach(t => aNamespace.AddElement(t));
            ns.Interfaces?.Select(t => MapReflectedTypeFromXML(t))?.ToList().ForEach(t => aNamespace.AddElement(t));
            ns.ValueTypes?.Select(t => MapReflectedTypeFromXML(t))?.ToList().ForEach(t => aNamespace.AddElement(t));

            return aNamespace;
        }

        private static ReflectedType MapReflectedTypeFromXML(XMLReflectedType t)
        {
            if (t == null)
            {
                return null;
            }

            if(!_modelDictionary.ContainsKey(t.Name))
            {
                _modelDictionary.Add(t.Name, new ReflectedType(t.Name, t.Namespace));

                ReflectedType type = new ReflectedType(t.Name, t.Namespace)
                {
                    Access = (AccessModifier)t.Access,
                    Attributes = t.Attributes,
                    BaseType = MapReflectedTypeFromXML(t.BaseType),
                    Constructors = t.Constructors?.Select(c => MapMethodFromXML(c))?.ToList(),
                    IsAbstract = t.IsAbstract,
                    Fields = t.Fields?.Select(f => MapFieldFromXML(f))?.ToList(),
                    ImplementedInterfaces = t.ImplementedInterfaces?.Select(i => MapReflectedTypeFromXML(i))?.ToList(),
                    IsStatic = t.IsStatic,
                    Methods = t.Methods?.Select(m => MapMethodFromXML(m))?.ToList(),
                    Properties = t.Properties?.Select(p => MapPropertyFromXML(p))?.ToList(),
                    TypeKind = (Kind)t.TypeKind
                };

                return type;
            }

            return _modelDictionary[t.Name];
        }

        private static Method MapMethodFromXML(XMLMethodModel m)
        {
            if (m == null)
                return null;
            return new Method
            {
                Access = (AccessModifier)m.Access,
                Name = m.Name,
                Parameters = m.Parameters?.Select(p => MapParameterFromXML(p))?.ToList(),
                ReturnType = MapReflectedTypeFromXML(m.ReturnType)
            };
        }

        private static Field MapFieldFromXML(XMLFieldModel f)
        {
            return (f == null) ? null : new Field
            {
                Access = (AccessModifier)f.Access,
                Name = f.Name,
                Type = MapReflectedTypeFromXML(f.Type)
            };
        }

        private static Property MapPropertyFromXML(XMLPropertyModel p)
        {
            return (p == null) ? null : new Property
            {
                PropertyAccess = (Property.Access)p.PropertyAccess,
                GetMethod = MapMethodFromXML(p.GetMethod),
                SetMethod = MapMethodFromXML(p.SetMethod),
                Name = p.Name,
                Type = MapReflectedTypeFromXML(p.Type),
            };
        }

        private static Parameter MapParameterFromXML(XMLParameterModel p)
        {
            return (p == null) ? null : new Parameter
            {
                Name = p.Name,
                ParamType = MapReflectedTypeFromXML(p.ParamType)
            };
        }
    }
}
