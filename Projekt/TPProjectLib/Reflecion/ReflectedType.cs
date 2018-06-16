using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Reflection
{
    [DataContract(Namespace = "", IsReference = true)]
    public class ReflectedType
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Namespace { get; private set; }
        [DataMember]
        public bool IsStatic { get; private set; }
        [DataMember]
        public bool IsAbstract { get; private set; }
        [DataMember]
        public Kind TypeKind { get; private set; }
        [DataMember]
        public AccessModifier Access { get; private set; }
        [DataMember]
        public ReflectedType BaseType { get; private set; }

        public List<Attribute> Attributes { get; private set; }
        [DataMember]
        public List<Method> Constructors { get; private set; }
        [DataMember]
        public List<Method> Methods { get; private set; }
        [DataMember]
        public List<Field> Fields { get; private set; }
        [DataMember]
        public List<Property> Properties { get; private set; }
        [DataMember]
        public List<ReflectedType> ImplementedInterfaces { get; private set; }

        public ReflectedType(Type type, Dictionary<string, ReflectedType> dictionary)
        {
            Name = type.Name;
            Namespace = type.Namespace;
            TypeKind = GetKind(type);
            IsStatic = GetIsStatic(type);
            IsAbstract = GetIsAbstract(type);
            Access = GetAccess(type);
            BaseType = GetBaseType(type, dictionary);

            Constructors = GetConstructors(type);
            Methods = GetMethods(type, dictionary);
            Fields = GetFields(type, dictionary);
            Properties = GetProperties(type, dictionary);
            ImplementedInterfaces = GetImplementedInterfaces(type, dictionary);
            Attributes = type.GetCustomAttributes(false).Cast<Attribute>().ToList();
        }

        public ReflectedType(string name, string ns)
        {
            Name = name;
            Namespace = ns;
        }


        private Kind GetKind(Type type)
        {
            return type.IsEnum ? Kind.Enum : type.IsInterface ? Kind.Interface : type.IsValueType ? Kind.Struct : Kind.Class;
        }

        private bool GetIsStatic(Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }

        private bool GetIsAbstract(Type type)
        {
            return type.IsAbstract;
        }

        private AccessModifier GetAccess(Type type)
        {
            if (type.IsPublic || type.IsNestedPublic)
                return AccessModifier.Public;

            if (type.IsNestedFamily)
                return AccessModifier.Protected;

            if (type.IsNestedFamANDAssem)
                return AccessModifier.ProtectedInternal;

            return AccessModifier.Private;
        }

        private List<ReflectedType> GetImplementedInterfaces(Type type, Dictionary<string, ReflectedType> dictionary)
        {
            List<ReflectedType> ifaces = (from Type iface in type.GetInterfaces() select new ReflectedType(iface, dictionary)).ToList();
            List<ReflectedType> implemented = new List<ReflectedType>();
            foreach(ReflectedType iface in ifaces)
            {
                if (!dictionary.ContainsKey(iface.Name))
                    dictionary.Add(iface.Name, new ReflectedType(iface.Name, iface.Namespace));

                implemented.Add(dictionary[iface.Name]);
            }
            return implemented;
        }

        private List<Method> GetConstructors(Type type)
        {
            return (from ConstructorInfo ctor in type.GetConstructors() select new Method(ctor)).ToList();
        }

        private List<Method> GetMethods(Type type, Dictionary<string, ReflectedType> dictionary)
        {
            List<Method> methods = (from MethodInfo method in type.GetMethods() select new Method(method, dictionary)).ToList();
            foreach(Method method in methods)
            {
                if (!dictionary.ContainsKey(method.ReturnType.Name))
                    dictionary.Add(method.ReturnType.Name, new ReflectedType(method.ReturnType.Name, method.ReturnType.Namespace));
            }
            return methods;
        }

        private List<Property> GetProperties(Type type, Dictionary<string, ReflectedType> dictionary)
        {
            List<Property> properties = (from PropertyInfo property in type.GetProperties() select new Property(property, dictionary)).ToList();
            foreach(var item in properties)
            {
                if (!dictionary.ContainsKey(item.Type.Name))
                    dictionary.Add(item.Type.Name, new ReflectedType(item.Type.Name, item.Type.Namespace));
            }
            return properties;
        }

        private List<Field> GetFields(Type type, Dictionary<string, ReflectedType> dictionary)
        {
            List<Field> fields = (from FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) select new Field(field, dictionary)).ToList();
            foreach(Field field in fields)
            {
                if (!dictionary.ContainsKey(field.Type.Name))
                    dictionary.Add(field.Type.Name, new ReflectedType(field.Type.Name, field.Type.Namespace));
            }

            return fields;
        }

        private ReflectedType GetBaseType(Type type, Dictionary<string, ReflectedType> dictionary)
        {
            if (type.BaseType != null)
            {
                if (!dictionary.ContainsKey(type.BaseType.Name))
                    dictionary.Add(type.BaseType.Name, new ReflectedType(type.BaseType.Name, type.BaseType.Namespace));

                return dictionary[type.BaseType.Name];
            }

            return null;
        }

        public void AnalyzeType(Type type, Dictionary<string, ReflectedType> dictionary)
        {
            TypeKind = GetKind(type);
            IsStatic = GetIsStatic(type);
            IsAbstract = GetIsAbstract(type);
            Access = GetAccess(type);
            BaseType = GetBaseType(type, dictionary);

            Constructors = GetConstructors(type);
            Methods = GetMethods(type, dictionary);
            Fields = GetFields(type, dictionary);
            Properties = GetProperties(type, dictionary);
            ImplementedInterfaces = GetImplementedInterfaces(type, dictionary);
            Attributes = type.GetCustomAttributes(false).Cast<Attribute>().ToList();
        }
    }
}
