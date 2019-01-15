using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using BusinessLogic;
using BusinessLogic.Reflection;
using GalaSoft.MvvmLight.Command;
using MEF;

namespace ViewModels.ViewModel
{
    public class ReflectionViewModel : ViewModelBase
    {
        private string _fileName;

        public RelayCommand SaveXMLCommand { get; }
        public RelayCommand LoadXMLCommand { get; }
        public RelayCommand LoadCommand { get; }

        [Import(typeof(IFileLoader))]
        public IFileLoader FileLoader { get; set; }
        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }
        public string FileName { get => _fileName; set { _fileName = value; InitializeReflectionModel();  Reflect(); } }
        public ReflectionModel Reflection { get; private set; }
        public ObservableCollection<NamespaceViewModel> Namespaces { get; private set; }
        public SerializationService SerializationService { get; set; }


        public ReflectionViewModel()
        {
            TabTitle = "Reflection";
            Namespaces = new ObservableCollection<NamespaceViewModel>();
            SerializationService = new SerializationService();
            LoadCommand = new RelayCommand(() => { FileName = FileLoader.LoadFile(); });
            SaveXMLCommand = new RelayCommand(() => { SerializationService.Serialize(Reflection, "reflectionmodel.xml"); });
        }

        public void Reflect()
        {            
            foreach (Namespace ns in Reflection.Namespaces)
            {
                Namespaces.Add(new NamespaceViewModel(ns));
            }
        }

        private void InitializeReflectionModel()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                Logger.Log(LogMode.Critical, "User didn't pick a DLL");
                return;
            }

            Reflection = new ReflectionModel(FileName);
        }
    }
}
