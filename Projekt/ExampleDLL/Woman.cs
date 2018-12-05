using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL
{
    [DataContract(Namespace = "")]
    public class Woman : Person
    {
        public Woman(string firstname, int height, double weight, Sex sex) : base(firstname, height, weight, sex)
        {
        }
    }
}
