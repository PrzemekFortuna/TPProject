using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TPProject.ViewModel
{
    public class TreeViewItemViewModel : INotifyPropertyChanged
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
            Children = new ObservableCollection<TreeViewItemViewModel>();
            Children.Add(this);
        }

        public virtual void LoadChildren()
        {

        }

    }
}
