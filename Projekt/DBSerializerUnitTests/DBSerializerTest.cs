using System;
using DBSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DBSerializerUnitTests
{
    [TestClass]
    public class DBSerializerTest
    {
        [TestMethod]
        public void DBSerializerConstructorTest()
        {
            DbSerializer serializer = new DbSerializer();

            Assert.IsNotNull(serializer);
            Assert.IsNotNull(serializer.Context);
        }
    }
}
