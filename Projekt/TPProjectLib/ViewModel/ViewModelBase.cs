using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModels.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private string _tabTitle;
        public string TabTitle { get { return _tabTitle; } set { _tabTitle = value; NotifyPropertyChanged(); } }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
