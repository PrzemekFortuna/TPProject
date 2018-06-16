using System;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fillers;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class ReaderTest
    {
        [TestMethod]
        public void ReaderConstructor()
        {
            Reader tmp = new Reader("Tomasz", "Kowalski", 2);
            Assert.AreEqual("Tomasz", tmp.Name);
            Assert.AreEqual("Kowalski", tmp.Surname);
            Assert.AreEqual(2, tmp.PersonalID);
        }

        [TestMethod]
        public void GetReaderTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Reader tmp = new Reader("Tomasz", "Kowalski", 2);
            Assert.AreEqual(tmp.GetHashCode(), dataRepository.GetReader(2).GetHashCode());
        }

        [TestMethod]
        public void UpdateReaderTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Reader tmp = new Reader("Marcin", "Nowak", 2);
            dataRepository.UpdateReader(2, tmp);
            Assert.AreEqual(tmp.GetHashCode(), dataRepository.GetReader(2).GetHashCode());
        }

        [TestMethod]
        public void AddReaderTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Reader tmp = new Reader("Marcin", "Nowak", 65);
            dataRepository.AddReader(tmp);
            Assert.AreEqual(tmp.GetHashCode(), dataRepository.GetReader(65).GetHashCode());
        }

        [TestMethod]
        public void GetAllReaderTest()
        {
            DataRepository dataRepository = new DataRepository();
            Reader tmp = new Reader("Marcin", "Nowak", 65);
            dataRepository.AddReader(tmp);
            Reader tmp2 = new Reader("Jan", "Nowacki", 3);
            dataRepository.AddReader(tmp2);

            List<Reader> list = dataRepository.GetAllReader();

            Assert.AreEqual(list[0].GetHashCode(), tmp.GetHashCode());
            Assert.AreEqual(list[1].GetHashCode(), tmp2.GetHashCode());
        }

        [TestMethod]
        public void DeleteReaderTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            dataRepository.DeleteReader(dataRepository.GetReader(2));
            Assert.IsNull(dataRepository.GetReader(2));
        }
    }
}
