using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using TPProjectLib.Model;
using TPProjectLib.Utility;

namespace TPProject.ViewModel
{
    public class TreeViewViewModel : ViewModelBase
    {
        private StudentViewModel _root;

        public ObservableCollection<StudentViewModel> FirstStudents { get; private set; }
        public RelayCommand SerializeCommand { get; set; }

        public TreeViewViewModel()
        {
            TabTitle = "Recursion";
            Student rootStudent = new Student("RootStudent");
            _root = new StudentViewModel(rootStudent);

            FirstStudents = new ObservableCollection<StudentViewModel>();
            FirstStudents.Add(_root);

            SerializeCommand = new RelayCommand(() => { XMLSerializer serializer = new XMLSerializer(); serializer.Serialize(rootStudent); LogManager.Log(LogMode.Info, "Serialization of TreeView circular reference was successful!"); }, true);
        }
    }
}
