using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class State //Stan biblioteki
    {
        public Book book { get; set; }
        public int Quantity { get; set; }

        public State(Book bookItem, int quantity)
        {
            this.book = bookItem;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return book.ToString() + Environment.NewLine
                   + "Quantity: " + Quantity;
        }
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Quantity.GetHashCode();
            hash = (hash * 7) + book.GetHashCode();
            return hash;
        }
    }
}
