using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Borrowing
    {
        public Reader Reader { get; set; }
        public DateTime Date { get; set; }
        public State State { get; set; }

        public Borrowing(Reader reader, DateTime date, State state)
        {
            Reader = reader;
            Date = date;
            State = state;
        }
        public override string ToString()
        {
            return "Reader: " + Reader.ToString() + Environment.NewLine
                    + "Date: " + Date.ToString() + Environment.NewLine
                    + "State: " + State.ToString();
        }
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + this.State.GetHashCode();
            hash = (hash * 7) + this.Reader.GetHashCode();
            return hash;
        }
    }
}
