using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL.Animals
{
    [DataContract(Namespace = "")]
    public class Dog : Animal, ISpeaking
    {
        public Dog(string name, DateTime birth) : base(name, birth)
        {
        }

        public string Sound(int number)
        {
            string sound = string.Empty;
            for (int i = 0; i < number; i++)
                sound += "woof ";

            return sound;
        }
    }
}
