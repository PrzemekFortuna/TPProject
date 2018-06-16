using ExampleDLL.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDLL
{
    public class Person
    {
        public enum Sex { Female, Male};

        public string FirstName { get; set; }
        public int Height { get; }
        public double Weight { get; private set; }
        public Sex PersonSex { get; set; }
        public int age;
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
