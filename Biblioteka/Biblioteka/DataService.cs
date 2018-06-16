using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class DataService
    {
        private DataRepository _repository = null;
        public DataService(DataRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException();
            this._repository = repository;
        }
        
        public Dictionary<int, Book> GetAllBooks()
        {
            return _repository.GetAllBooks();
        }

        public IEnumerable<Borrowing> AllBorrowingsForBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            return from events in _repository.GetAllBorrowing()
                   where events.State.book == book
                   select events;
        }

        public IEnumerable <Borrowing> AllBorrowingsBetweenDates(DateTime start, DateTime end)
        {
            return from events in _repository.GetAllBorrowing()
                   where events.Date.Date >= start.Date && events.Date.Date <= end.Date
                   select events;
        }

        public void AddBook(Book book)
        {
            _repository.AddBook(book);
        }

        public void DeleteBook(int key)
        {
            _repository.DeleteBook(key);
        }

        public void UpdateBook(int key, Book book)
        {
            _repository.UpdateBook(key, book);
        }

        public Book GetBook(int key)
        {
            return _repository.GetBook(key);
        }

        public void AddReader(Reader reader)
        {
            _repository.AddReader(reader);
        }

        public void DeleteReader(Reader reader)
        {
            _repository.DeleteReader(reader);
        }

        public List<Reader> GetAllReaders()
        {
            return _repository.GetAllReader();
        }

        public void UpdateReader(int id, Reader reader)
        {
            _repository.UpdateReader(id, reader);
        }

        public Reader GetReader(int id)
        {
            return _repository.GetReader(id);
        }

        public void AddState(State state)
        {
            _repository.AddState(state);
        }
        public void DeleteState(State state)
        {
            _repository.DeleteState(state);
        }

        public void UpdateState(int id, State state)
        {
            _repository.UpdateState(id, state);
        }

        public State GetState(int id)
        {
            return _repository.GetState(id);
        }

        public List<State> GetAllState()
        {
            return _repository.GetAllState();
        }

        public void AddBorrowing (Reader reader, State state)
        {
            if (reader == null || state == null)
                throw new ArgumentNullException();
            Borrowing tmp = new Borrowing(reader, DateTime.Now.Date, state);
            _repository.GetAllBorrowing().CollectionChanged += OnAddCollectionChanged;
            _repository.AddBorrowing(tmp);
        }

        public void DeleteBorrowing(Borrowing borrowing)
        {
            _repository.GetAllBorrowing().CollectionChanged += OnDeleteCollectionChanged;
            _repository.DeleteBorrowing(borrowing);
        }

        public void UpdateBorrowing(int personalID, int bookID, Borrowing borrowing)
        {
            _repository.UpdateBorrowing(personalID, bookID, borrowing);
        }

        public Borrowing GetBorrowing(int pID, int bID)
        {
            return _repository.GetBorrowing(pID, bID);
        }

        public ObservableCollection<Borrowing> GetAllBorrowing()
        {
            return _repository.GetAllBorrowing();
        }

        public void PrintRelatedData (IEnumerable<Borrowing> borrowings)
        {
            if (borrowings == null)
                throw new ArgumentNullException();

            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach(Borrowing x in borrowings)
            {
                sb.Append("Borrowing #" + i + ":");
                sb.Append(Environment.NewLine);
                sb.Append(x.ToString());
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                i++;
            }
            Console.WriteLine(sb.ToString(0, sb.Length - 1));
        }

        public void PrintCatalog(IDictionary <int, Book> positions)
        {
            if (positions == null)
                throw new ArgumentNullException();

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int,Book> x in positions)
            {
                sb.Append(x.Key);
                sb.Append(":");
                sb.Append(x.Value.BookID);
                sb.Append(",");
                sb.Append(x.Value.Name);
                sb.Append(Environment.NewLine);
            }           
            Console.WriteLine(sb.ToString(0, sb.Length - 1));
        }

        public void OnDeleteCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.OldItems != null)
            {
                foreach(Borrowing element in e.OldItems)
                {
                    _repository.UpdateState(element.State.book.BookID, new State(element.State.book, element.State.Quantity+1));
                }
            }
        }

        public void OnAddCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach(Borrowing element in e.NewItems)
                {
                    _repository.UpdateState(element.State.book.BookID, new State(element.State.book, element.State.Quantity - 1));
                }
            }
        }


    }
}
