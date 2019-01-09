using BaseModel.Reflection;
using BusinessLogic.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BusinessLogic
{
    public static class ModelMapper
    {
        public static Dictionary<string, BaseReflectedType> BaseTypes = new Dictionary<string, BaseReflectedType>();
        public static Dictionary<string, ReflectedType> Types = new Dictionary<string, ReflectedType>();

        public static ReflectionModel MapUp(BaseReflectionModel model)
        {
            ReflectionModel reflectionModel = new ReflectionModel();
            Type type = model.GetType();

            PropertyInfo namespacesProperty = type.GetProperty("Namespaces", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseNamespaceModel> namespaces = (List<BaseNamespaceModel>)ConvertList(typeof(BaseNamespaceModel), (IList)namespacesProperty?.GetValue(model));

            if(namespaces != null)
            {

            }

            return reflectionModel;
        }

        private static Namespace NamespaceUp(BaseNamespaceModel model)
        {
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

        private static ReflectedType MapTypeUp(BaseReflectedType model)
        {
            if (model == null)
                return null;

            ReflectedType type = new ReflectedType();
            
            if(!Types.ContainsKey(model.Name))
            {
                Types.Add(model.Name, type);
                //FillType(model, type);
            }

            return Types[model.Name];
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
