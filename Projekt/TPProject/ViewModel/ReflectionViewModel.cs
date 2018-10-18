using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using TPProjectLib.Reflection;
using TPProjectLib.Utility;

namespace TPProject.ViewModel
{
    public class ReflectionViewModel : ViewModelBase
    {
        public RelayCommand LoadCommand { get; }
        public RelayCommand OpenDialogCommand { get; }
        public OpenFileDialog DLLDialog { get; private set; }
        public ReflectionModel Reflection { get; private set; }
        public ObservableCollection<NamespaceViewModel> Namespaces { get; private set; }


        public ReflectionViewModel()
        {
            TabTitle = "Reflection";
            DLLDialog = new OpenFileDialog();
            LoadCommand = new RelayCommand(() => { XMLSerializer serializer = new XMLSerializer(); serializer.Serialize(Reflection, "reflecionxml.xml"); LogManager.Log(LogMode.Info, "Serialization of reflection model successful"); }, true);
            OpenDialogCommand = new RelayCommand(() => { DLLDialog.ShowDialog(); Reflect(); }, true);
            Namespaces = new ObservableCollection<NamespaceViewModel>();
        }

        public void Reflect()
        {
            if(DLLDialog.FileName == string.Empty)
            {
                LogManager.Log(LogMode.Critical, "User didn't pick a DLL");
                return;
            }

            Reflection = new ReflectionModel(DLLDialog.FileName);
            foreach(Namespace ns in Reflection.Namespaces)
            {
                Namespaces.Add(new NamespaceViewModel(ns));
            }
        }
    }
}
