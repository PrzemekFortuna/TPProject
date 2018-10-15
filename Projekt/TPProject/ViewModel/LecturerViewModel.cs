using System.Collections.ObjectModel;
using System.Linq;
using TPProjectLib.Model;

namespace TPProject.ViewModel
{
    public class LecturerViewModel : TreeViewItemViewModel
    {
        private Lecturer _lecturer;
        public override string Name { get { return _lecturer.Name; } }

        public LecturerViewModel(Lecturer lecturer)
        {
            _lecturer = lecturer;
        }

        public override void LoadChildren()
        {
            Children.Clear();
            foreach(Student element in _lecturer.Students)
            {
                Children.Add(new StudentViewModel(element));
            }
        }
    }
}
