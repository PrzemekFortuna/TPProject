using System.Runtime.Serialization;

namespace ExampleDLL
{
    [DataContract(Namespace = "", IsReference = true)]
    public class ClassA
    {   
        [DataMember]
        public ClassB classB { get; set; }
    }
}
