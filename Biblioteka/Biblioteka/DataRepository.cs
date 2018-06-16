using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class DataRepository
    {
        private IFiller _filler = null;
        private KontekstDanych _kontekstDanych = new KontekstDanych();
        
        public DataRepository(IFiller filler)
        {
            _filler = filler;
            filler.Fill(_kontekstDanych);
        }
        public DataRepository()
        {
        }
        
        //Reader
        public void AddReader(Reader Reader)
        {
            if (Reader != null)
                _kontekstDanych.clientList.Add(Reader);
            else
                throw new ArgumentNullException();
        }

        public Reader GetReader(int personalID)
        {
            try
            {
                return _kontekstDanych.clientList.Find(x => x.PersonalID == personalID);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public List<Reader> GetAllReader()
        {
            return _kontekstDanych.clientList;
        }

        public void UpdateReader(int personalID, Reader Reader)
        {
            if(Reader == null)
                throw new ArgumentNullException();

            Reader tmp = _kontekstDanych.clientList.First(x => x.PersonalID == personalID);
            if (tmp != null)
            {
                tmp.Name = Reader.Name;
                tmp.Surname = Reader.Surname;
                tmp.PersonalID = Reader.PersonalID;
            }
        }

        public void DeleteReader(Reader Reader)
        {
            _kontekstDanych.clientList.Remove(Reader);
        }

        //Book
        
        public void AddBook(Book katalog)
        {
            if (katalog != null)
                _kontekstDanych.BooksDictionary.Add(katalog.BookID, katalog);
            else
                throw new ArgumentNullException();
        }

        public Book GetBook(int key)
        {
            try
            {
                return _kontekstDanych.BooksDictionary[key];
            }
            catch(KeyNotFoundException)
            {
                return null;
            }
        }

        public Dictionary<int, Book> GetAllBooks ()
        {
            return _kontekstDanych.BooksDictionary;
        }

        public void UpdateBook(int key, Book katalog)
        {
            if (katalog != null)
                _kontekstDanych.BooksDictionary[key] = katalog;
            else
                throw new ArgumentNullException();
        }
        
        public void DeleteBook(int key)
        {
            _kontekstDanych.BooksDictionary.Remove(key);
        }

        //State

        public void AddState(State opis)
        {
            if (opis != null)
                _kontekstDanych.statesList.Add(opis);
            else
                throw new ArgumentNullException();
        }

        public State GetState(int BookID)
        {
            return _kontekstDanych.statesList.Find(x => x.book.BookID == BookID);
        }

        public List<State> GetAllState()
        {
            return _kontekstDanych.statesList;
        }

        public void UpdateState(int BookID, State opisStanu)
        {
            if (opisStanu == null)
                throw new ArgumentNullException();

            State tmp = GetState(BookID);

            if(tmp != null)
            {
                tmp.book = opisStanu.book;
                tmp.Quantity = opisStanu.Quantity;
            }
        }

        public void DeleteState(State opis)
        {
            _kontekstDanych.statesList.Remove(opis);
        }
        
        //Borrowing
        public void AddBorrowing(Borrowing borrowing)
        {
            if (borrowing.State.Quantity < 1)
                throw new Exception("Brak ksiazki!");
            else
                borrowing.State.Quantity--;

            if (borrowing != null)
            {
                _kontekstDanych.borrowingsCollection.Add(borrowing);
            }
            else
                throw new ArgumentNullException();
        }

        public Borrowing GetBorrowing(int personalID, int bookID)
        {
            try
            {   
                //Linq lambda expression
                IEnumerable<Borrowing> tmp = _kontekstDanych.borrowingsCollection.Where(a => a.Reader.PersonalID == personalID).Where(b => b.State.book.BookID == bookID);
                return tmp.First();
            }
            catch(InvalidOperationException)
            {
                return null;
            }
        }

        public ObservableCollection<Borrowing> GetAllBorrowing()
        {
            return _kontekstDanych.borrowingsCollection;
        }

        public void UpdateBorrowing(int personaID, int bookID, Borrowing borrowing)
        {
            Borrowing tmp = this.GetBorrowing(personaID, bookID);

            if (tmp != null)
            {
                tmp.Reader = borrowing.Reader;
                tmp.Date = borrowing.Date;
                tmp.State = borrowing.State;
            }

        }

        public void DeleteBorrowing(Borrowing borrowing)
        {
            _kontekstDanych.borrowingsCollection.Remove(borrowing);
        }

    }
}
