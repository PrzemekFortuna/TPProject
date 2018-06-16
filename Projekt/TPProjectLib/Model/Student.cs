using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace TPProjectLib.Model
{
    [DataContract(Namespace = "", IsReference = true)]
    public class Student
    {   
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ObservableCollection<Lecturer> Lecturers { get; set; }

        public Student(string name)
        {
            Name = name;
            Lecturers = new ObservableCollection<Lecturer>();
            Lecturers.Add(new Lecturer("lecturer1", this));
            Lecturers.Add(new Lecturer("lecturer2", this));
            Lecturers.Add(new Lecturer("lecturer3", this));
        }
        public Student(string name, Lecturer lect)
        {
            Name = name;
            Lecturers = new ObservableCollection<Lecturer>();
            Lecturers.Add(lect);
        }
    }
}
