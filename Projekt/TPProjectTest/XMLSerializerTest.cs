using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TPProjectLib.Reflection;
using TPProjectLib.Utility;

namespace TPProjectTest
{
    [TestClass]
    public class XMLSerializerTest
    {
        //[TestMethod]
        //public void RecursionSerializationDeserializationTest()
        //{
        //    XMLSerializer serializer = new XMLSerializer();
        //    Student student = new Student("Zbyszek");
        //    serializer.Serialize(student);

        //    Student student2 = serializer.Deserialize("xmlfile.xml");

        //    Assert.AreEqual(student.Name, student2.Name);
        //    Assert.AreEqual(student.Lecturers.First().Name, student2.Lecturers.First().Name);
        //}

        [TestMethod]
        public void ReflectionSerializationDeserializationTest()
        {
            XMLSerializer serializer = new XMLSerializer();
            ReflectionModel reflection = new ReflectionModel(Path.GetFullPath(@"..\\..\\..\\ExampleDLL\\bin\\Debug\\ExampleDLL.dll"));

            serializer.Serialize(reflection);

            ReflectionModel deserialized = serializer.Deserialize("reflectionmodel.xml");

            Assert.AreEqual("ExampleDLL", deserialized.Namespaces.Find(x => x.Name == "ExampleDLL").Name);
            Assert.AreEqual("ExampleDLL.Animals", deserialized.Namespaces.Find(x => x.Name == "ExampleDLL.Animals").Name);

            List<string> classes = (from tmp_classes in deserialized.Namespaces
                          from tmp_class in tmp_classes.Classes
                          select tmp_class.Name).ToList();

            Assert.IsTrue(classes.Contains("Person"));
            Assert.IsTrue(classes.Contains("StaticClass"));
            Assert.IsTrue(classes.Contains("Woman"));
            Assert.IsTrue(classes.Contains("PrivateClass"));
            Assert.IsTrue(classes.Contains("Animal"));
            Assert.IsTrue(classes.Contains("Cat"));
            Assert.IsTrue(classes.Contains("Dog"));
        }

    }
}
