using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer.Reflection;
using ExampleDLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializers;
using Serializers.XMLModel;

namespace UnitTestProject1
{
    [TestClass]
    public class XMLSerializerTest
    {
        [TestMethod]
        public void RecursionSerializationDeserializationTest()
        {
            XMLSerializer<ClassA> serializer = new XMLSerializer<ClassA>();
            ClassA test = new ClassA();
            test.classB = new ClassB();
            serializer.Serialize(test, "xmlfile.xml");

            ClassA test2 = serializer.Deserialize("xmlfile.xml");

            Assert.IsNotNull(test2.classB);
        }

        [TestMethod]
        public void ReflectionSerializationDeserializationTest()
        {
            XMLSerializer<XMLReflectionModel> serializer = new XMLSerializer<XMLReflectionModel>();
            ReflectionModel reflection = new ReflectionModel(Path.GetFullPath(@"..\\..\\..\\ExampleDLL\\bin\\Debug\\ExampleDLL.dll"));

            serializer.Serialize(XMLMapper.MapToXMLModel(reflection), "reflectionmodel.xml");

            XMLReflectionModel deserializedXML = serializer.Deserialize("reflectionmodel.xml");

            ReflectionModel deserialized = XMLMapper.MapFromXMLModel(deserializedXML);

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
