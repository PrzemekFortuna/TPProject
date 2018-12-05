using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL.Animals
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class Animal
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public TimeSpan Age { get { return (DateTime.Now - DateOfBirth); } set => Age = value; }
        [DataMember]
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
