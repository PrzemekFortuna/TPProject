using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL
{
    public class Woman : Person
    {
        public Woman(string firstname, int height, double weight, Sex sex) : base(firstname, height, weight, sex)
        {
        }
    }
}
