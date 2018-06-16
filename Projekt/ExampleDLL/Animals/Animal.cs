using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL.Animals
{
    [Serializable]
    public class Animal
    {
        public string Name { get; set; }
        public TimeSpan Age { get { return (DateTime.Now - DateOfBirth); } set => Age = value; }
        public DateTime DateOfBirth { get; set; }

        public Animal(string name, DateTime birth)
        {
            Name = name;
            DateOfBirth = birth;
        }

        public void Eat(string food)
        {
        }

        public bool Play(string toy)
        {
            return true;
        }
    }
}
