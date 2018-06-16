using System;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fillers;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class BookTest
    {
        [TestMethod]
        public void BookConstructor()
        {
            Book book = new Book(1, "Apokalipsa");
            Assert.AreEqual(1, book.BookID);
            Assert.AreEqual("Apokalipsa", book.Name);
        }

        [TestMethod]
        public void GetBookTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Assert.AreEqual("Pasja C++",  dataRepository.GetBook(2).Name);
        }

        [TestMethod]
        public void UpdateBookTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            dataRepository.UpdateBook(2, new Book(2, "Myszka"));
            Assert.AreEqual("Myszka", dataRepository.GetBook(2).Name);
        }

        [TestMethod]
        public void AddBookTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Book tmp = new Book(5, "Fotel");
            dataRepository.AddBook(tmp);
            Assert.AreEqual(tmp.GetHashCode(), dataRepository.GetBook(tmp.BookID).GetHashCode());
        }

        [TestMethod]
        public void GetAllBookTest()
        {
            DataRepository dataRepository = new DataRepository();
            Book tmp = new Book(5, "Fotel");
            Book tmp2 = new Book(8, "Motel");
            dataRepository.AddBook(tmp);
            dataRepository.AddBook(tmp2);

            Dictionary<int, Book> BooksDictionary = dataRepository.GetAllBooks();

            Book outtmp = null;
            BooksDictionary.TryGetValue(tmp.BookID,out outtmp);
            Book outtmp2 = null;
            BooksDictionary.TryGetValue(tmp2.BookID, out outtmp2);
            Assert.AreEqual(outtmp.GetHashCode(), tmp.GetHashCode());
        }

        [TestMethod]
        public void DeleteBookTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            dataRepository.DeleteBook(1);
            Assert.IsNull(dataRepository.GetBook(1));
        }
    }
}
