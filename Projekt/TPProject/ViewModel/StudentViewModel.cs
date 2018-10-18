using System.Collections.ObjectModel;
using System.Linq;
using TPProjectLib.Model;

namespace TPProject.ViewModel
{
    public class StudentViewModel : TreeViewItemViewModel
    {
        private Student _student;

        public override string Name { get { return _student.Name; } }

        public StudentViewModel(Student student)
        {
            _student = student;
        }

        public override void LoadChildren()
        {
            Children.Clear();
            foreach(Lecturer element in _student.Lecturers)
            {
                Children.Add(new LecturerViewModel(element));
            }
        }
    }
}
