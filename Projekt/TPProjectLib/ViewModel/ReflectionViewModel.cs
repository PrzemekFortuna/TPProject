using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using TPProjectLib.Reflection;
using TPProjectLib.Utility;
using TPProjectLib.Utility.FileLoaders;

namespace TPProject.ViewModel
{
    public class ReflectionViewModel : ViewModelBase
    {
        private string _fileName;

        public RelayCommand SaveCommand { get; }
        public RelayCommand LoadCommand { get; }
        public IFileLoader FileLoader { get; set; }
        private ISerializer<ReflectionModel> _serializer;
        public string FileName { get => _fileName; set { _fileName = value;  Reflect(); } }
        public ReflectionModel Reflection { get; private set; }
        public ObservableCollection<NamespaceViewModel> Namespaces { get; private set; }


        public ReflectionViewModel()
        {
            TabTitle = "Reflection";
            SaveCommand = new RelayCommand(() => { _serializer.Serialize(Reflection); LogManager.Log(LogMode.Info, "Serialization of reflection model successful"); }, true);            
            Namespaces = new ObservableCollection<NamespaceViewModel>();
            LoadCommand = new RelayCommand(() => { FileName = FileLoader.LoadFile(); });
        }

        public void Reflect()
        {
            if (FileName == string.Empty)
            {
                LogManager.Log(LogMode.Critical, "User didn't pick a DLL");
                return;
            }

            Reflection = new ReflectionModel(FileName);
            foreach (Namespace ns in Reflection.Namespaces)
            {
                Namespaces.Add(new NamespaceViewModel(ns));
            }
        }
    }
}
