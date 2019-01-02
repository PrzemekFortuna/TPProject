using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModels.ViewModel
{
    public abstract class TreeViewItemViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Properties
        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                if(value != _isExpanded)
                {
                    _isExpanded = value;
                    NotifyPropertyChanged();
                }

                if(!WasBuild)
                {
                    LoadChildren();
                }
            }
        }

        public virtual string Name { get; }

        public bool WasBuild { get; set; }

        public ObservableCollection<TreeViewItemViewModel> Children { get; set; }

        #endregion

        public TreeViewItemViewModel()
        {
            Children = new ObservableCollection<TreeViewItemViewModel> {this};
        }

        public abstract void LoadChildren();

    }
}
