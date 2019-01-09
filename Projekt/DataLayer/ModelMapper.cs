using BaseModel.Reflection;
using BusinessLogic.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static BaseModel.Reflection.BasePropertyModel;

namespace BusinessLogic
{
    public static class ModelMapper
    {
        public static Dictionary<string, BaseReflectedType> BaseTypes = new Dictionary<string, BaseReflectedType>();
        public static Dictionary<string, ReflectedType> Types = new Dictionary<string, ReflectedType>();

        public static ReflectionModel MapUp(BaseReflectionModel model)
        {
            if (model == null)
                return null;
            ReflectionModel reflectionModel = new ReflectionModel();
            Type type = model.GetType();

            PropertyInfo namespacesProperty = type.GetProperty("Namespaces", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseNamespaceModel> namespaces = (List<BaseNamespaceModel>)ConvertList(typeof(BaseNamespaceModel), (IList)namespacesProperty?.GetValue(model));

            if (namespaces != null)
            {
                reflectionModel.Namespaces = namespaces.Select(NamespaceUp).ToList();
            }

            return reflectionModel;
        }

        public static BaseReflectionModel MapDown(ReflectionModel model, Type reflectionModelType)
        {
            if (model == null)
                return null;
            object reflectionModel = Activator.CreateInstance(reflectionModelType);

            PropertyInfo nameProperty = reflectionModelType.GetProperty("Name");
            PropertyInfo namespaceModelsProperty = reflectionModelType.GetProperty("NamespaceModels",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            namespaceModelsProperty?.SetValue(
                reflectionModel,
                ConvertList(namespaceModelsProperty.PropertyType.GetGenericArguments()[0],
                    model.Namespaces.Select(n => NamespaceDown(n, namespaceModelsProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            return (BaseReflectionModel)reflectionModel;
        }


        private static Namespace NamespaceUp(BaseNamespaceModel model)
        {
            if (model == null)
                return null;
            Namespace ns = new Namespace(model.Name);
            Type type = model.GetType();
            PropertyInfo classesProperty = type.GetProperty("Classes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo valueTypesProperty = type.GetProperty("ValueTypes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo interfacesProperty = type.GetProperty("Interfaces", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseReflectedType> classes = (List<BaseReflectedType>)ConvertList(typeof(BaseReflectedType), (IList)classesProperty?.GetValue(model));
            List<BaseReflectedType> valueTypes = (List<BaseReflectedType>)ConvertList(typeof(BaseReflectedType), (IList)valueTypesProperty?.GetValue(model));
            List<BaseReflectedType> interfaces = (List<BaseReflectedType>)ConvertList(typeof(BaseReflectedType), (IList)interfacesProperty?.GetValue(model));

            if (classes != null)
            {
                ns.Classes = classes.Select(c => MapTypeUp(c)).ToList();
                ns.Interfaces = interfaces.Select(i => MapTypeUp(i)).ToList();
                ns.ValueTypes = valueTypes.Select(vt => MapTypeUp(vt)).ToList();
            }

            return ns;
        }

        private static BaseNamespaceModel NamespaceDown(Namespace model, Type namespaceModelType)
        {
            if (model == null)
                return null;
            object namespaceModel = Activator.CreateInstance(namespaceModelType);
            PropertyInfo nameProperty = namespaceModelType.GetProperty("Name");
            PropertyInfo classesProperty = namespaceModelType.GetProperty("Classes",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo valueTypesProperty = namespaceModelType.GetProperty("ValueTypes",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo interfacesProperty = namespaceModelType.GetProperty("Interfaces",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(namespaceModel, model.Name);

            classesProperty?.SetValue(namespaceModel,
                ConvertList(classesProperty.PropertyType.GetGenericArguments()[0],
                    model.Classes.Select(c => MapTypeDown(c, classesProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            valueTypesProperty?.SetValue(namespaceModel,
                ConvertList(valueTypesProperty.PropertyType.GetGenericArguments()[0],
                    model.ValueTypes.Select(v => MapTypeDown(v, valueTypesProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            interfacesProperty?.SetValue(namespaceModel,
                ConvertList(interfacesProperty.PropertyType.GetGenericArguments()[0],
                    model.Interfaces.Select(i => MapTypeDown(i, interfacesProperty.PropertyType.GetGenericArguments()[0])).ToList()));

            return (BaseNamespaceModel)namespaceModel;
        }

        private static ReflectedType MapTypeUp(BaseReflectedType model)
        {
            if (model == null)
                return null;

            ReflectedType type = new ReflectedType();

            if (!Types.ContainsKey(model.Name))
            {
                FillType(model, type);
                Types.Add(model.Name, type);
            }

            return Types[model.Name];
        }

        private static BaseReflectedType MapTypeDown(ReflectedType model, Type typeModelType)
        {
            if (model == null)
                return null;
            object type = Activator.CreateInstance(typeModelType);
            BaseReflectedType baseType = null;
            if (!BaseTypes.ContainsKey(model.Name))
            {
                FillBaseType(model, baseType);
                BaseTypes.Add(model.Name, baseType);
            }
            return BaseTypes[model.Name];
        }

        private static void FillType(BaseReflectedType model, ReflectedType reflectedType)
        {
            reflectedType.Name = model.Name;
            reflectedType.Access = (AccessModifier)model.Access;
            reflectedType.IsAbstract = model.IsAbstract;
            reflectedType.IsStatic = model.IsStatic;
            reflectedType.Namespace = model.Namespace;
            reflectedType.TypeKind = (Kind)model.TypeKind;

            Type type = model.GetType();

            PropertyInfo baseTypeProperty = type.GetProperty("BaseType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseReflectedType baseType = (BaseReflectedType)baseTypeProperty?.GetValue(model);
            reflectedType.BaseType = MapTypeUp(baseType);

            PropertyInfo methodsProperty = type.GetProperty("Methods", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (methodsProperty?.GetValue(model) != null)
            {
                List<BaseMethodModel> methods = (List<BaseMethodModel>)ConvertList(typeof(BaseReflectedType), (IList)methodsProperty?.GetValue(model));
                reflectedType.Methods = methods?.Select(MapMethodUp).ToList();
            }

            PropertyInfo constructorsProperty = type.GetProperty("Constructors", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (methodsProperty?.GetValue(model) != null)
            {
                List<BaseMethodModel> constructors = (List<BaseMethodModel>)ConvertList(typeof(BaseReflectedType), (IList)methodsProperty?.GetValue(model));
                reflectedType.Methods = constructors?.Select(MapMethodUp).ToList();
            }

            PropertyInfo fieldsProperty = type.GetProperty("Fields", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (fieldsProperty?.GetValue(model) != null)
            {
                List<BaseFieldModel> fields = (List<BaseFieldModel>)ConvertList(typeof(BaseParameterModel),
                        (IList)fieldsProperty?.GetValue(model));
                reflectedType.Fields = fields?.Select(MapFieldUp).ToList();
            }

            PropertyInfo propertiesProperty = type.GetProperty("Properties", BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            if (propertiesProperty?.GetValue(model) != null)
            {
                List<BasePropertyModel> properties = (List<BasePropertyModel>)ConvertList(typeof(BasePropertyModel), (IList)propertiesProperty?.GetValue(model));
                reflectedType.Properties = properties.Select(MapPropertyUp).ToList();
            }

            PropertyInfo implementedInterfacesProperty = type.GetProperty("ImplementedInterfaces", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (implementedInterfacesProperty?.GetValue(model) != null)
            {
                List<BaseReflectedType> interfaces = (List<BaseReflectedType>)ConvertList(typeof(BaseReflectedType), (IList)implementedInterfacesProperty?.GetValue(model));
                reflectedType.ImplementedInterfaces = interfaces?.Select(MapTypeUp).ToList();
            }

            //Attributes                                                
        }

        private static void FillBaseType(ReflectedType model, BaseReflectedType typeModel)
        {
            Type typeModelType = typeModel.GetType();

            typeModelType.GetProperty("Name")?.SetValue(typeModel, model.Name);
            typeModelType.GetProperty("IsAbstract")?.SetValue(typeModel, model.IsAbstract);
            typeModelType.GetProperty("IsStatic")?.SetValue(typeModel, model.IsStatic);
            typeModelType.GetProperty("TypeKind")?.SetValue(typeModel, (BaseKindModel)model.TypeKind);
            typeModelType.GetProperty("AccessModifier")?.SetValue(typeModel, (BaseAccessModifier)model.Access);
            typeModelType.GetProperty("Namespace")?.SetValue(typeModel, model.Namespace);

            if (model.BaseType != null)
            {
                typeModelType.GetProperty("BaseType",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    ?.SetValue(typeModel, typeModelType.Cast(MapTypeDown(model.BaseType, typeModelType)));
            }

            if (model.Attributes != null)
            {
                typeModelType.GetProperty("Attributes",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    ?.SetValue(typeModel, model.Attributes);
            }

            if (model.ImplementedInterfaces != null)
            {
                PropertyInfo implementedInterfacesProperty = typeModelType.GetProperty("ImplementedInterfaces",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                implementedInterfacesProperty?.SetValue(typeModel,
                    ConvertList(typeModelType,
                        model.ImplementedInterfaces?.Select(c =>
                            typeModelType.Cast(MapTypeDown(c.BaseType, typeModelType))).ToList()));
            }

            if (model.Fields != null)
            {
                PropertyInfo fieldsProperty = typeModelType.GetProperty("Fields",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                fieldsProperty?.SetValue(typeModel,
                    ConvertList(fieldsProperty.PropertyType.GetGenericArguments()[0],
                        model.Fields?.Select(c =>
                           MapFieldDown(c,
                                fieldsProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }

            if (model.Methods != null)
            {
                PropertyInfo methodsProperty = typeModelType.GetProperty("Methods",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                methodsProperty?.SetValue(typeModel,
                    ConvertList(methodsProperty.PropertyType.GetGenericArguments()[0],
                        model.Methods?.Select(m =>
                                MapMethodDown(m,
                                    methodsProperty?.PropertyType.GetGenericArguments()[0]))
                            .ToList()));
            }

            if (model.Constructors != null)
            {
                PropertyInfo constructorsProperty = typeModelType.GetProperty("Constructors",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                constructorsProperty?.SetValue(typeModel,
                    ConvertList(constructorsProperty.PropertyType.GetGenericArguments()[0],
                        model.Constructors?.Select(c =>
                            MapMethodDown(c,
                                constructorsProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }

            if (model.Properties != null)
            {
                PropertyInfo propertiesProperty = typeModelType.GetProperty("Properties",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                propertiesProperty?.SetValue(typeModel,
                    ConvertList(propertiesProperty.PropertyType.GetGenericArguments()[0],
                        model.Properties?.Select(c =>
                            MapPropertyDown(c,
                                propertiesProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }
        }

        private static Property MapPropertyUp(BasePropertyModel p)
        {
            Property property = new Property();
            property.Name = p.Name;
            property.PropertyAccess = (Property.Access)p.PropertyAccess;
            Type type = p.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type", BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            BaseReflectedType reflectedType = (BaseReflectedType)typeProperty?.GetValue(p);

            if (reflectedType != null)
                property.Type = MapTypeUp(reflectedType);

            PropertyInfo setMethodProperty = type.GetProperty("SetMethod", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            BaseMethodModel setMethod = (BaseMethodModel)setMethodProperty?.GetValue(p);
            if (setMethod != null)
                property.SetMethod = MapMethodUp(setMethod);

            PropertyInfo getMethodProperty = type.GetProperty("GetMethod", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            BaseMethodModel getMethod = (BaseMethodModel)setMethodProperty?.GetValue(p);
            if (getMethod != null)
                property.GetMethod = MapMethodUp(getMethod);

            return property;
        }

        private static BasePropertyModel MapPropertyDown(Property model, Type propertyModelType)
        {
            object propertyModel = Activator.CreateInstance(propertyModelType);
            propertyModelType.GetProperty("Name")?.SetValue(propertyModel, model.Name);
            PropertyInfo typeProperty = propertyModelType.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            propertyModelType.GetProperty("PropertyAccess")?.SetValue(model, model.PropertyAccess);
            PropertyInfo setMethodProperty = propertyModelType.GetProperty("SetMethod", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            if (setMethodProperty != null)
                setMethodProperty?.SetValue(propertyModel, MapMethodDown(model.SetMethod, setMethodProperty?.PropertyType.GetGenericArguments()[0]));

            PropertyInfo getMethodProperty = propertyModelType.GetProperty("SetMethod", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            if (getMethodProperty != null)
                getMethodProperty?.SetValue(propertyModel, MapMethodDown(model.GetMethod, setMethodProperty?.PropertyType.GetGenericArguments()[0]));

            if (model.Type != null)
                typeProperty?.SetValue(propertyModel,
                    typeProperty.PropertyType.Cast(MapTypeDown(model.Type, typeProperty.PropertyType)));

            return (BasePropertyModel)propertyModel;
        }

        private static Field MapFieldUp(BaseFieldModel f)
        {
            Field field = new Field();
            field.Name = f.Name;

            Type type = f.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseReflectedType reflectedType = (BaseReflectedType)typeProperty?.GetValue(f);
            if (reflectedType != null)
                field.Type = MapTypeUp(reflectedType);

            return field;
        }


        private static BaseFieldModel MapFieldDown(Field model, Type fieldModelType)
        {
            object fieldModel = Activator.CreateInstance(fieldModelType);
            fieldModelType.GetProperty("Name")?.SetValue(fieldModel, model.Name);
            PropertyInfo typeProperty = fieldModelType.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            fieldModelType.GetProperty("Access")?.SetValue(model, model.Access);

            if (model.Type != null)
                typeProperty?.SetValue(fieldModel,
                    typeProperty.PropertyType.Cast(MapTypeDown(model.Type, typeProperty?.PropertyType)));

            return (BaseFieldModel) fieldModel;
        }


        private static Method MapMethodUp(BaseMethodModel m)
        {
            Method method = new Method();
            method.Name = m.Name;
            method.Access = (AccessModifier)m.Access;
            Type type = m.GetType();

            PropertyInfo parametersProperty = type.GetProperty("Parameters", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (parametersProperty?.GetValue(m) != null)
            {
                List<BaseParameterModel> parameters = (List<BaseParameterModel>)ConvertList(typeof(BaseParameterModel), (IList)parametersProperty?.GetValue(m));
                method.Parameters = parameters.Select(MapParameterUp).ToList();
            }

            PropertyInfo returnTypeProperty = type.GetProperty("ReturnType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseReflectedType returnType = (BaseReflectedType)returnTypeProperty?.GetValue(m);
            if (returnType != null)
                method.ReturnType = MapTypeUp(returnType);

            return method;
        }

        private static BaseMethodModel MapMethodDown(Method model, Type methodModelType)
        {
            object methodModel = Activator.CreateInstance(methodModelType);
            PropertyInfo nameProperty = methodModelType.GetProperty("Name");
            PropertyInfo returnTypeProperty = methodModelType.GetProperty("ReturnType",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            methodModelType?.GetProperty("Access")?.SetValue(model, model.Access);
            PropertyInfo parametersProperty = methodModelType.GetProperty("Parameters",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            
            nameProperty?.SetValue(methodModel, model.Name);
            if (model.ReturnType != null)
                returnTypeProperty?.SetValue(methodModel,
                    returnTypeProperty.PropertyType.Cast(MapTypeDown(model.ReturnType, returnTypeProperty?.PropertyType)));

            if (model.Parameters != null)
                returnTypeProperty?.SetValue(methodModel, 
                    ConvertList(parametersProperty?.PropertyType.GetGenericArguments()[0],
                    model.Parameters?.Select(c =>
                        MapParameterDown(c,
                            parametersProperty?.PropertyType.GetGenericArguments()[0])).ToList()));

            return (BaseMethodModel)methodModel;
        }

        private static Parameter MapParameterUp(BaseParameterModel p)
        {
            Parameter parameter = new Parameter();
            parameter.Name = p.Name;
            Type type = p.GetType();

            PropertyInfo paramTypeProperty = type.GetProperty("ParamType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseReflectedType paramType = (BaseReflectedType)paramTypeProperty?.GetValue(p);

            if (paramType != null)
                parameter.ParamType = MapTypeUp(paramType);

            return parameter;
        }

        private static BaseParameterModel MapParameterDown(Parameter model, Type parameterModelType)
        {
            object parameterModel = Activator.CreateInstance(parameterModelType);
            PropertyInfo nameProperty = parameterModelType.GetProperty("Name");
            PropertyInfo typeProperty = parameterModelType.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(parameterModel, model.Name);
            if (model.ParamType != null)
                typeProperty?.SetValue(parameterModel,
                    typeProperty.PropertyType.Cast(MapTypeDown(model.ParamType, typeProperty?.PropertyType)));

            return (BaseParameterModel)parameterModel;
        }

        private static IList ConvertList(Type type, IList source)
        {
            var listType = typeof(List<>);
            Type[] typeArgs = { type };
            var genericListType = listType.MakeGenericType(typeArgs);
            var typedList = (IList)Activator.CreateInstance(genericListType);
            foreach (var item in source)
            {
                typedList.Add(type.Cast(item));
            }

            return typedList;

        }

        public static object Cast(this Type Type, object data)
        {
            ParameterExpression DataParam = Expression.Parameter(typeof(object), "data");
            BlockExpression Body = Expression.Block(Expression.Convert(Expression.Convert(DataParam, data.GetType()), Type));

            Delegate Run = Expression.Lambda(Body, DataParam).Compile();
            object ret = Run.DynamicInvoke(data);
            return ret;
        }
    }
}
