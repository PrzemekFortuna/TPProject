using System;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fillers;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class StateTest
    {
        [TestMethod]
        public void StateConstructor()
        {
            State tmp = new State(new Book(7, "Apokalipsa"), 5);
            Assert.AreEqual(tmp.book.BookID, 7);
            Assert.AreEqual(tmp.book.Name, "Apokalipsa");
            Assert.AreEqual(tmp.Quantity, 5);
        }


        [TestMethod]
        public void GetStateTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            State tmp = new State(dataRepository.GetBook(1), 10);
            Assert.AreEqual(tmp.GetHashCode(), dataRepository.GetState(1).GetHashCode());
        }

        [TestMethod]
        public void UpdateStateTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            dataRepository.UpdateState(1, new State(dataRepository.GetBook(1), 20));
            Assert.AreEqual(new State(dataRepository.GetBook(1), 20).GetHashCode(), dataRepository.GetState(1).GetHashCode());
        }

        [TestMethod]
        public void AddOpisStanuTest()
        {
            DataRepository dataRepository = new DataRepository();
            State tmp = new State(new Book(7, "Pilot"), 5);
            dataRepository.AddState(tmp);
            Assert.AreEqual(tmp.GetHashCode(), dataRepository.GetState(7).GetHashCode());
        }

        [TestMethod]
        public void GetAllOpisStanuTest()
        {
            DataRepository dataRepository = new DataRepository();
            State tmp = new State(new Book(7, "Pilot"), 5);
            dataRepository.AddState(tmp);
            State tmp2 = new State(new Book(6, "Biblia"), 11);
            dataRepository.AddState(tmp2);
            
            List<State> list = dataRepository.GetAllState();

            State value = list.First(x => x.book.BookID == 7);
            State value2 = list.First(x => x.book.BookID == 6);

            Assert.AreEqual(tmp.GetHashCode(), value.GetHashCode());
            Assert.AreEqual(tmp2.GetHashCode(), value2.GetHashCode());

        }

        [TestMethod]
        public void DeleteStateTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            dataRepository.DeleteState(dataRepository.GetState(2));
            Assert.IsNull(dataRepository.GetState(2));
        }
    }
}
