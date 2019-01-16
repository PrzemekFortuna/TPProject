using System.Collections.Generic;
using System.IO;
using System.Linq;
using BaseModel.Reflection;
using BusinessLogic;
using BusinessLogic.Reflection;
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
        public void ReflectionSerializationDeserializationTest()
        {
            XMLSerializer serializer = new XMLSerializer();
            //BaseReflectionModel model = new XMLReflectionModel();
  
            //ReflectionModel mod = new ReflectionModel();
            //mod.Namespaces = new List<Namespace> { new Namespace("A namespace") };
            //serializer.Serialize(ModelMapper.MapDown(mod, model.GetType()), "reflectionmodel.xml");
            //ReflectionModel deserialized = ModelMapper.MapUp(serializer.Deserialize("reflectionmodel.xml"));

            //Assert.IsTrue(deserialized != null);

        }

    }
}
