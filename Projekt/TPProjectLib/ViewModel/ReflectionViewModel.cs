using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight.Command;
using Serializers;
using Serializers.XMLModel;
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
        [Import(typeof(ISerializer<XMLReflectionModel>))]
        private ISerializer<XMLReflectionModel> _serializer;
        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }
        public string FileName { get => _fileName; set { _fileName = value;  Reflect(); } }
        public ReflectionModel Reflection { get; private set; }
        public ObservableCollection<NamespaceViewModel> Namespaces { get; private set; }


        public ReflectionViewModel()
        {
            TabTitle = "Reflection";
            SaveCommand = new RelayCommand(() => { _serializer.Serialize(XMLMapper.MapToXMLModel(Reflection), "xmlfile.xml"); Logger.Log(LogMode.Info, "Serialization of reflection model successful"); }, true);            
            Namespaces = new ObservableCollection<NamespaceViewModel>();
            LoadCommand = new RelayCommand(() => { FileName = FileLoader.LoadFile(); });
        }

        public void Reflect()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                Logger.Log(LogMode.Critical, "User didn't pick a DLL");
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
