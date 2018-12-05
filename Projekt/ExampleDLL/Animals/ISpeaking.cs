using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL.Animals
{
    interface ISpeaking
    {
        string Sound(int number);
    }
}
