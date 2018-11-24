using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
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
        public IFileLoader FileLoader { get; set; } = new WPFFileLoader();
        public string FileName { get => _fileName; set { _fileName = value;  Reflect(); } }
        public ReflectionModel Reflection { get; private set; }
        public ObservableCollection<NamespaceViewModel> Namespaces { get; private set; }


        public ReflectionViewModel()
        {
            TabTitle = "Reflection";
            SaveCommand = new RelayCommand(() => { XMLSerializer serializer = new XMLSerializer(); serializer.Serialize(Reflection, "reflecionxml.xml"); LogManager.Log(LogMode.Info, "Serialization of reflection model successful"); }, true);            
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
