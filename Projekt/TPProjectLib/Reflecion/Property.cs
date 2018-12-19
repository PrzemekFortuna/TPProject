﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TPProjectLib.Utility;

namespace TPProjectLib.Reflection
{
    public class Property
    {
        public enum Access
        {
            ReadOnly,
            ReadWrite
        }

        public string Name { get; private set; }
        public Access PropertyAccess { get; private set; }
        public Method SetMethod { get; private set; }
        public Method GetMethod { get; private set; }
        public ReflectedType Type { get; private set; }

        public Property(PropertyInfo info)
        {
            Name = info.Name;
            Type = SingletonDictionary<ReflectedType>.Types[info.PropertyType.Name];
            PropertyAccess = (info.CanRead && (!info.CanWrite)) ? Access.ReadOnly : Access.ReadWrite;
            GetSetMethods(info);
        }

        private void GetSetMethods(PropertyInfo info)
        {
            if(info.SetMethod != null)
                SetMethod = new Method(info.SetMethod);
            if(info.GetMethod != null)
                GetMethod = new Method(info.GetMethod);
        }

        private ReflectedType GetType(PropertyInfo info)
        {
            if(!SingletonDictionary<ReflectedType>.Types.ContainsKey(info.PropertyType.Name))
                SingletonDictionary<ReflectedType>.Types.Add(info.PropertyType.Name, new ReflectedType(info.PropertyType.Name, info.PropertyType.Namespace));

            return SingletonDictionary<ReflectedType>.Types[info.PropertyType.Name];
        }

    }
}
