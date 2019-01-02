using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer;
using DataLayer.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReflectionUnitTests
{
    [TestClass]
    public class ReflectionTest
    {
        private string URL = @"..\\..\\..\\ExampleDLL\\bin\\Debug\\ExampleDLL.dll";

        [TestMethod]
        public void NamespacesTest()
        {
            ReflectionModel reflect = new ReflectionModel(Path.GetFullPath(URL));

            Assert.AreEqual("ExampleDLL", reflect.Namespaces.Find(x => x.Name == "ExampleDLL").Name);
            Assert.AreEqual("ExampleDLL.Animals", reflect.Namespaces.Find(x => x.Name == "ExampleDLL.Animals").Name);
        }


        [TestMethod]
        public void ClassesNamesTest()
        {
            ReflectionModel reflect = new ReflectionModel(Path.GetFullPath(URL));

            IEnumerable<string> tmp_names = from names in reflect.Namespaces
                                          from classes in names.Classes
                                          select classes.Name;
            
            Assert.AreEqual(tmp_names.ToList().Find(x => x == "Cat"), "Cat");
            Assert.AreEqual(tmp_names.ToList().Find(x => x == "Dog"), "Dog");
            Assert.AreEqual(tmp_names.ToList().Find(x => x == "Animal"), "Animal");
            Assert.AreEqual(tmp_names.ToList().Find(x => x == "Person"), "Person");
            Assert.AreEqual(tmp_names.ToList().Find(x => x == "StaticClass"), "StaticClass");
            Assert.AreEqual(tmp_names.ToList().Find(x => x == "Woman"), "Woman");
            Assert.AreEqual(tmp_names.ToList().Find(x => x == "PrivateClass"), "PrivateClass");
        }

        [TestMethod]
        public void ClassTest()
        {
            ReflectionModel reflect = new ReflectionModel(Path.GetFullPath(URL));
            ReflectedType cat = (from names in reflect.Namespaces
                                 from classes in names.Classes
                                 where classes.Name == "Cat"
                                 select classes).First();

            Assert.AreEqual("Sound", cat.Methods.Find(x => x.Name == "Sound").Name);
            Assert.AreEqual(AccessModifier.Public, cat.Methods.Find(x => x.Name == "Sound").Access);
            Assert.AreEqual("String", cat.Methods.Find(x => x.Name == "Sound").ReturnType.Name);
            Assert.AreEqual("Int32", cat.Methods.Find(x => x.Name == "Sound").Parameters.First().ParamType.Name);
            Assert.AreEqual(AccessModifier.Public, cat.Access);
            Assert.IsFalse(cat.IsAbstract);
            Assert.IsFalse(cat.IsStatic);
            Assert.AreEqual(Kind.Class, cat.TypeKind);
            Assert.AreEqual("ISpeaking", cat.ImplementedInterfaces.First().Name);
            Assert.AreEqual(1, cat.Constructors.Count);
            Assert.AreEqual("ExampleDLL.Animals", cat.Namespace);
            Assert.IsTrue(cat.Properties.Exists(x => x.Name == "Name"));
            Assert.AreEqual("Animal", cat.BaseType.Name);
            
        }

        [TestMethod]
        public void InterfacesTest()
        {
            ReflectionModel reflect = new ReflectionModel(Path.GetFullPath(URL));

            ReflectedType a_interface = (from names in reflect.Namespaces
                                from inter in names.Interfaces
                                select inter).First();

                        Assert.AreEqual("ISpeaking", a_interface.Name);
            Assert.IsTrue(a_interface.IsAbstract);
            Assert.AreEqual(a_interface.TypeKind.ToString(), "Interface");
        }

        [TestMethod]
        public void ValueTypeTest()
        {
            ReflectionModel reflecion = new ReflectionModel(Path.GetFullPath(URL));
            ReflectedType reflectedType = reflecion.Namespaces.Find(x => x.Name == "ExampleDLL").ValueTypes.Find(x => x.Name == "Point");

            Assert.AreEqual("Point", reflectedType.Name);
            Assert.AreEqual("ExampleDLL", reflectedType.Namespace);
            Assert.AreEqual("ValueType", reflectedType.BaseType.Name);
            Assert.AreEqual("x", reflectedType.Fields[0].Name);
            Assert.AreEqual(AccessModifier.Private, reflectedType.Fields[0].Access);
            Assert.AreEqual("Int32", reflectedType.Fields[0].Type.Name);
            Assert.AreEqual("y", reflectedType.Fields[1].Name);
            Assert.AreEqual(AccessModifier.Private, reflectedType.Fields[1].Access);
            Assert.AreEqual("Int32", reflectedType.Fields[1].Type.Name);
            Assert.AreEqual("GetX", reflectedType.Methods.Find(x => x.Name == "GetX").Name);
            Assert.AreEqual("GetY", reflectedType.Methods.Find(x => x.Name == "GetY").Name);
            Assert.AreEqual("Equals", reflectedType.Methods.Find(x => x.Name == "Equals").Name);
            Assert.AreEqual("GetHashCode", reflectedType.Methods.Find(x => x.Name == "GetHashCode").Name);
            Assert.AreEqual("ToString", reflectedType.Methods.Find(x => x.Name == "ToString").Name);
            Assert.AreEqual("GetType", reflectedType.Methods.Find(x => x.Name == "GetType").Name);
            Assert.AreEqual(6, reflectedType.Methods.Count);
        }

        [TestMethod]
        public void DictionaryTest()
        {
            ReflectionModel model = new ReflectionModel(Path.GetFullPath(URL));

            Assert.IsTrue(SingletonDictionary<ReflectedType>.Types.ContainsKey("Cat"));
            Assert.IsTrue(SingletonDictionary<ReflectedType>.Types.ContainsKey("Dog"));
            Assert.IsTrue(SingletonDictionary<ReflectedType>.Types.ContainsKey("Animal"));
            Assert.IsTrue(SingletonDictionary<ReflectedType>.Types.ContainsKey("Person"));
            Assert.IsTrue(SingletonDictionary<ReflectedType>.Types.ContainsKey("Woman"));
            Assert.IsTrue(SingletonDictionary<ReflectedType>.Types.ContainsKey("StaticClass"));
            Assert.IsTrue(SingletonDictionary<ReflectedType>.Types.ContainsKey("PrivateClass"));

        }
    }
}
