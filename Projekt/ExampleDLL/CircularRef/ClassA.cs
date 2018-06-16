using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL.CircularRef
{
    public class ClassA
    {
        public ClassB ClassB;

        public ClassA()
        {
            ClassB = new ClassB();
        }
    }
}
