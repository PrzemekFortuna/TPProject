using BaseModel.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializers;

namespace UnitTestProject1
{
    [TestClass]
    public class XMLSerializerTest
    {

        [TestMethod]
        public void ReflectionSerializationDeserializationNameTest()
        {
            XMLSerializer serializer = new XMLSerializer();
            BaseReflectionModel model = new BaseReflectionModel();

            model.Name = "TestName";

            serializer.Serialize(model, "test1model.xml");
            BaseReflectionModel deserialized = serializer.Deserialize("test1model.xml");

            Assert.IsTrue(deserialized != null);
            Assert.AreEqual(model.Name, deserialized.Name);

        }

        [TestMethod]
        public void ReflectionSerializationDeserializationNamespacesTest()
        {
            XMLSerializer serializer = new XMLSerializer();
            BaseReflectionModel model = new BaseReflectionModel();

            model.Namespaces = new System.Collections.Generic.List<BaseNamespaceModel> { new BaseNamespaceModel(),
            new BaseNamespaceModel(), new BaseNamespaceModel()};

            serializer.Serialize(model, "test2model.xml");
            BaseReflectionModel deserialized = serializer.Deserialize("test2model.xml");

            Assert.IsTrue(deserialized != null);
            Assert.AreEqual(model.Namespaces.Count, deserialized.Namespaces.Count);

        }

    }
}
