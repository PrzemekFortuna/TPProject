using System;
using Fillers;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject
{
    [TestClass]
    public class XMLFillerTest
    {
        [TestMethod]
        public void CheckIfFillingSucceeded()
        {
            DataRepository dataRepository = new DataRepository(new XMLFiller("xmlfile"));

            Assert.AreNotEqual(0, dataRepository.GetAllBooks().Count);
            Assert.AreNotEqual(0, dataRepository.GetAllReader().Count);
        }
    }
}
