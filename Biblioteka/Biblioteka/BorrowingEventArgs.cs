using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class BorrowingEventArgs : EventArgs
    {
        private readonly Book _book;
        public Book book { get { return _book; } }

        public BorrowingEventArgs(Book book)
        {
            _book = book;
        }


    }
}
