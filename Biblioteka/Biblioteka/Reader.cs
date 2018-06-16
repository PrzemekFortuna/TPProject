using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Reader
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PersonalID { get; set; }

        public Reader(string name, string surname, int personalID)
        {
            Name = name;
            Surname = surname;
            PersonalID = personalID;
        }

        public override string ToString()
        {
            return Name + " " + Surname + ", ID: " + PersonalID;
        }
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + this.Name.GetHashCode();
            hash = (hash * 7) + this.Surname.GetHashCode();
            hash = (hash * 7) + this.PersonalID.GetHashCode();
            return hash;
        }
    }
}
