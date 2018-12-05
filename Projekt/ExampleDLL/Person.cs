using ExampleDLL.Animals;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL
{
    [DataContract(Namespace = "")]
    public class Person
    {

        public enum Sex {
            [EnumMember]
            Female,
            [EnumMember]
            Male };
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public int Height { get; }
        [DataMember]
        public double Weight { get; private set; }
        [DataMember]
        public Sex PersonSex { get; set; }
        [DataMember]
        public int age;
        [DataMember]
        private List<Animal> _animals;

        public Person(string firstname, int height, double weight, Sex sex)
        {
            FirstName = firstname;
            Height = height;
            Weight = weight;
            PersonSex = sex;
            _animals = new List<Animal>();
        }

        public void BuyAnimal(Animal animal)
        {
            _animals.Add(animal);
        }

        public void FeedAnimal(string animalName, string food)
        {
            _animals.Find(x => x.Name == animalName).Eat(food);
        }
        
    }
}
