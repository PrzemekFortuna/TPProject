using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public partial class KontekstDanych
    {
        public List<Reader> clientList = new List<Reader>();
        public Dictionary<int, Book> BooksDictionary= new Dictionary<int, Book>();
        public ObservableCollection<Borrowing> borrowingsCollection = new ObservableCollection<Borrowing>();
        public List<State> statesList = new List<State>();
    }
}
