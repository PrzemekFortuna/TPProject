using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
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

        [Import(typeof(IFileLoader))]
        public IFileLoader FileLoader { get; set; }
        [Import(typeof(ISerializer<ReflectionModel>))]
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
            if (FileName == string.Empty || FileName == null)
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
