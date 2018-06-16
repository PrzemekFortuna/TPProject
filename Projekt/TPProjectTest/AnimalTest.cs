using System;
using ExampleDLL.Animals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TPProjectTest
{
    [TestClass]
    public class AnimalTest
    {
        [TestMethod]
        public void SoundTest()
        {
            Dog dog = new Dog("Burek", new DateTime(2018, 1, 15));
            Cat cat = new Cat("Mruczek", new DateTime(2018, 2, 3));

            Assert.AreEqual("woof woof ", dog.Sound(2));
            Assert.AreEqual("meow meow meow ", cat.Sound(3));
        }
    }
}
