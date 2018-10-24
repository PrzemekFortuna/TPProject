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

        public ReflectedType(Type type)
        {
            Name = type.Name;
            Namespace = type.Namespace;
            TypeKind = GetKind(type);
            IsStatic = GetIsStatic(type);
            IsAbstract = GetIsAbstract(type);
            Access = GetAccess(type);
            BaseType = GetBaseType(type);

            Constructors = GetConstructors(type);
            Methods = GetMethods(type);
            Fields = GetFields(type);
            Properties = GetProperties(type);
            ImplementedInterfaces = GetImplementedInterfaces(type);
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

        private List<ReflectedType> GetImplementedInterfaces(Type type)
        {
            List<ReflectedType> ifaces = (from Type iface in type.GetInterfaces() select new ReflectedType(iface)).ToList();
            List<ReflectedType> implemented = new List<ReflectedType>();
            foreach(ReflectedType iface in ifaces)
            {
                if (!Globals.Types.ContainsKey(iface.Name))
                    Globals.Types.Add(iface.Name, new ReflectedType(iface.Name, iface.Namespace));

                implemented.Add(Globals.Types[iface.Name]);
            }
            return implemented;
        }

        private List<Method> GetConstructors(Type type)
        {
            return (from ConstructorInfo ctor in type.GetConstructors() select new Method(ctor)).ToList();
        }

        private List<Method> GetMethods(Type type)
        {
            List<Method> methods = (from MethodInfo method in type.GetMethods() select new Method(method)).ToList();
            foreach(Method method in methods)
            {
                if (!Globals.Types.ContainsKey(method.ReturnType.Name))
                    Globals.Types.Add(method.ReturnType.Name, new ReflectedType(method.ReturnType.Name, method.ReturnType.Namespace));
            }
            return methods;
        }

        private List<Property> GetProperties(Type type)
        {
            List<Property> properties = (from PropertyInfo property in type.GetProperties() select new Property(property)).ToList();
            foreach(var item in properties)
            {
                if (!Globals.Types.ContainsKey(item.Type.Name))
                    Globals.Types.Add(item.Type.Name, new ReflectedType(item.Type.Name, item.Type.Namespace));
            }
            return properties;
        }

        private List<Field> GetFields(Type type)
        {
            List<Field> fields = (from FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) select new Field(field)).ToList();
            foreach(Field field in fields)
            {
                if (!Globals.Types.ContainsKey(field.Type.Name))
                    Globals.Types.Add(field.Type.Name, new ReflectedType(field.Type.Name, field.Type.Namespace));
            }

            return fields;
        }

        private ReflectedType GetBaseType(Type type)
        {
            if (type.BaseType != null)
            {
                if (!Globals.Types.ContainsKey(type.BaseType.Name))
                    Globals.Types.Add(type.BaseType.Name, new ReflectedType(type.BaseType.Name, type.BaseType.Namespace));

                return Globals.Types[type.BaseType.Name];
            }

            return null;
        }

        public void AnalyzeType(Type type)
        {
            TypeKind = GetKind(type);
            IsStatic = GetIsStatic(type);
            IsAbstract = GetIsAbstract(type);
            Access = GetAccess(type);
            BaseType = GetBaseType(type);

            Constructors = GetConstructors(type);
            Methods = GetMethods(type);
            Fields = GetFields(type);
            Properties = GetProperties(type);
            ImplementedInterfaces = GetImplementedInterfaces(type);
            Attributes = type.GetCustomAttributes(false).Cast<Attribute>().ToList();
        }
    }
}
