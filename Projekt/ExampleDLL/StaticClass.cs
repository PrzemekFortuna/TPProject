using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL
{
    [DataContract(Namespace = "")]
    static class StaticClass
    {
        [DataContract(Namespace = "")]
        private class PrivateClass
        {

        }
    }
}
