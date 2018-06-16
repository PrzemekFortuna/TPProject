using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace TPProjectLib.Model
{
    [DataContract(Namespace ="", IsReference = true)]
    public class Lecturer
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ObservableCollection<Student> Students { get; set; }
        

        public Lecturer(string name,Student stu)
        {
            Name = name;
            Students = new ObservableCollection<Student>();
            Students.Add(stu);
        }
        public Lecturer(string name)
        {
            Name = name;
            Students = new ObservableCollection<Student>();
        }
    }
}
