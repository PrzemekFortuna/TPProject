using System;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fillers;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class BorrowingTest
    {
        [TestMethod]
        public void BorrowingConstructor()
        {
            Reader reader = new Reader("Jan", "Kowalski", 1);
            Book book = new Book(1, "Ksiega Dzungli");
            State state = new State(book, 10);
            DateTime date = new DateTime(2018,3,4);
            Borrowing borrowing = new Borrowing(reader, date, state);

            Assert.AreEqual(reader.GetHashCode(), borrowing.Reader.GetHashCode());
            Assert.AreEqual(state.GetHashCode(), borrowing.State.GetHashCode());
            Assert.AreEqual(date.GetHashCode(), borrowing.Date.GetHashCode());
        }
        [TestMethod]
        public void GetBorrowingTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());

            Borrowing tmp = new Borrowing(dataRepository.GetReader(2), new DateTime(2018, 3, 4), dataRepository.GetState(1));
            Borrowing tmp2 = dataRepository.GetBorrowing(dataRepository.GetReader(2).PersonalID, dataRepository.GetState(1).book.BookID);
            Assert.AreEqual(tmp.GetHashCode(), tmp2.GetHashCode()); 
        }

        [TestMethod]
        public void UpdateBorrowingTest()
        {
            DataRepository dataRepository = new DataRepository();

            Reader reader = new Reader("Jan", "Kowalski", 1);
            Book book = new Book(1, "Ksiega Dzungli");
            State state = new State(book, 10);

            Borrowing borrowing = new Borrowing(reader, new DateTime(2018, 3, 4), state);

            dataRepository.AddBorrowing(borrowing);

            Reader reader_modified = new Reader("Janusz", "Nowak", 2);
            Borrowing borrowing_modified = new Borrowing(reader_modified, new DateTime(2018, 3, 4), state);
            dataRepository.UpdateBorrowing(1,1,borrowing_modified);

            Assert.AreEqual(borrowing_modified.GetHashCode(), dataRepository.GetBorrowing(2, 1).GetHashCode());
        }

        [TestMethod]
        public void AddBorrowingTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Reader reader = new Reader("Jan", "Kowalski", 1);
            Book book =  new Book(1, "Ksiega Dzungli");
            State state = new State(book, 10);

            Borrowing borrowing = new Borrowing(reader, new DateTime(2018, 3, 4),state);

            dataRepository.AddBorrowing(borrowing);

            Assert.AreEqual(borrowing.GetHashCode(), dataRepository.GetBorrowing(1,1).GetHashCode());
        }

        [TestMethod]
        public void GetAllBorrowingTest()
        {
            DataRepository dataRepository = new DataRepository();
            Reader reader = new Reader("Jan", "Kowalski", 1);
            Book book = new Book(1, "Ksiega Dzungli");
            State state = new State(book, 10);

            Borrowing borrowing = new Borrowing(reader, new DateTime(2018, 3, 4), state);

            Reader reader2 = new Reader("John", "Smith", 2);
            Borrowing borrowing2 = new Borrowing(reader2, new DateTime(2018, 3, 4), state);

            dataRepository.AddBorrowing(borrowing);
            dataRepository.AddBorrowing(borrowing2);

            ObservableCollection<Borrowing> collection = dataRepository.GetAllBorrowing();

            Borrowing tmp1 = collection.First();
            Borrowing tmp2 = collection[1];
            Assert.AreEqual(tmp1.GetHashCode(), borrowing.GetHashCode());
            Assert.AreEqual(tmp2.GetHashCode(), borrowing2.GetHashCode());
        }

        [TestMethod]
        public void DeleteBorrowingTest()
        {
            DataRepository dataRepository = new DataRepository();
            Reader reader = new Reader("Jan", "Kowalski", 1);
            Book book = new Book(1, "Ksiega Dzungli");
            State state = new State(book, 10);

            Borrowing borrowing = new Borrowing(reader, new DateTime(2018, 3, 4), state);

            dataRepository.AddBorrowing(borrowing);

            dataRepository.DeleteBorrowing(borrowing);

            Assert.IsNull(dataRepository.GetBorrowing(1, 1));
        }
    }
}
