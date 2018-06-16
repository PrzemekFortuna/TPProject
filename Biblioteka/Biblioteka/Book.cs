using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Book
    {
        public int BookID { get; set; }
        public string Name { get; set; }

        public Book(int bookID, string name)
        {
            BookID = bookID;
            Name = name;
        }
        public override string ToString()
        {
            return "Title: " + Name + ", ID: " + BookID;
        }
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + BookID.GetHashCode();
            hash = (hash * 7) + Name.GetHashCode();
            return hash;
        }
    }
}
