using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL.CircularRef
{
    public class ClassB
    {
        public ClassA ClassA;

        public ClassB()
        {
            ClassA = new ClassA();
        }
    }
}
