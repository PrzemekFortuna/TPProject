using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biblioteka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fillers;

namespace Biblioteka.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        [TestMethod()]
        public void DataServiceTest()
        {
            DataRepository repo = new DataRepository();
            DataService service = new DataService(repo);

            Assert.IsNotNull(service);
        }

        [TestMethod()]
        public void AllBorrowingsForBookTest()
        {
            DataRepository repo = new DataRepository(new ConstFiller());
            DataService service = new DataService(repo);

            IEnumerable<Borrowing> borrowings = service.AllBorrowingsForBook(repo.GetBook(2));

            DateTime tmp = new DateTime(2018, 3, 5);
            Assert.AreEqual(borrowings.First().Reader.Name, "Janusz");
            Assert.AreEqual(borrowings.First().Date.Date, tmp.Date);
        }

        [TestMethod()]
        public void AllBorrowingsBetweenDatesTest()
        {
            DataRepository repo = new DataRepository(new ConstFiller());
            DataService service = new DataService(repo);

            DateTime start = new DateTime(2018, 3, 4);
            DateTime end = new DateTime(2018, 3, 5);

            IEnumerable<Borrowing> borrowings = service.AllBorrowingsBetweenDates(start, end);

        }

        [TestMethod()]
        public void ServiceCRUDBorrowingTest()
        {
            DataRepository repo = new DataRepository();
            DataService service = new DataService(repo);
            Borrowing tmp = new Borrowing(new Reader("Jan", "Nowak", 1), DateTime.Now.Date, new State(new Book(1, "JA"), 2)); 
            //Wypozyczenie powoduje Quantity--
            service.AddBorrowing(new Reader("Jan", "Nowak", 1), new State(new Book(1, "JA"), 3));

            Assert.AreEqual(tmp.GetHashCode(), service.GetBorrowing(1, 1).GetHashCode());
            Assert.AreEqual(tmp.GetHashCode(), service.GetAllBorrowing().First().GetHashCode());

            Borrowing tmp1 = new Borrowing(new Reader("Jan", "Nowak", 1), DateTime.Now.Date, new State(new Book(1, "JA"), 3));

            service.UpdateBorrowing(1, 1, tmp1);

            Assert.AreEqual(tmp1.GetHashCode(), service.GetBorrowing(1, 1).GetHashCode());

            service.DeleteBorrowing(service.GetBorrowing(1,1));

            Assert.IsNull(service.GetBorrowing(1, 1));
        }

        [TestMethod()]
        public void PrintRelatedDataTest()
        {
            DataRepository repo = new DataRepository(new ConstFiller());
            DataService service = new DataService(repo);

            try
            {
                service.PrintRelatedData(repo.GetAllBorrowing());
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void PrintCatalogTest()
        {
            DataRepository repo = new DataRepository(new ConstFiller());
            DataService service = new DataService(repo);

            try
            { 
            service.PrintCatalog(repo.GetAllBooks());
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ServiceCRUDBookTest()
        {
            DataRepository repo = new DataRepository();
            DataService service = new DataService(repo);

            Book tmp = new Book(1, "Pasja");
            service.AddBook(tmp);

            Assert.AreEqual(tmp.GetHashCode(), service.GetBook(1).GetHashCode());
            Assert.AreEqual(tmp.GetHashCode(), service.GetAllBooks().First().Value.GetHashCode());

            Book tmp1 = new Book(2, "Pasja");

            service.UpdateBook(1, tmp1);

            Assert.AreEqual(tmp1.GetHashCode(), service.GetBook(1).GetHashCode());

            service.DeleteBook(1);

            Assert.IsNull(service.GetBook(1));
        }

        [TestMethod()]
        public void ServiceCRUDReaderTest()
        {
            DataRepository repo = new DataRepository();
            DataService service = new DataService(repo);

            Reader tmp = new Reader("Jan", "Nowak", 1);
            service.AddReader(tmp);

            Assert.AreEqual(tmp.GetHashCode(), service.GetReader(1).GetHashCode());
            Assert.AreEqual(tmp.GetHashCode(), service.GetAllReaders().First().GetHashCode());

            Reader tmp1 = new Reader("Jan", "Nowak", 2);

            service.UpdateReader(1, tmp1);

            Assert.AreEqual(tmp1.GetHashCode(), service.GetReader(2).GetHashCode());

            service.DeleteReader(service.GetReader(2));

            Assert.IsNull(service.GetReader(2));
        }

        [TestMethod()]
        public void ServiceCRUDStateTest()
        {
            DataRepository repo = new DataRepository();
            DataService service = new DataService(repo);
            Book tmpB = new Book(1, "Pasja");
            State tmp = new State(tmpB, 20);

            service.AddState(tmp);

            Assert.AreEqual(tmp.GetHashCode(), service.GetAllState().First().GetHashCode());
            Assert.AreEqual(tmp.GetHashCode(), service.GetState(1).GetHashCode());

            State tmp1 = new State(tmpB, 1);

            service.UpdateState(1, tmp1);

            Assert.AreEqual(tmp1.GetHashCode(), service.GetState(1).GetHashCode());

            service.DeleteState(service.GetState(1));

            Assert.IsNull(service.GetState(1));
        }
    }
}