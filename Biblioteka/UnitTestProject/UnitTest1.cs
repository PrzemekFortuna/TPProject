using System;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetWykazTest()
        {
            DataRepository dataRepository = new DataRepository(new WypelnienieStalymi());
            Wykaz tmp = new Wykaz("Tomasz", "Kowalski", 2);

            Assert.AreEqual(tmp, dataRepository.GetWykaz(2));

        }
    }
}
