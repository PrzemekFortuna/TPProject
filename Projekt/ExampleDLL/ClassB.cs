using System.Runtime.Serialization;

namespace ExampleDLL
{
    [DataContract(Namespace = "", IsReference = true)]
    public class ClassB
    {
        [DataMember]
        public ClassA classA { get; set; }

    }
}