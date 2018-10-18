using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPProjectLib.Reflection;

namespace TPProject.ViewModel
{
    public class PropertyViewModel : TreeViewItemViewModel
    {
        private Property _property;
        public override string Name
        {
            get
            {
                return _property.PropertyAccess.ToString() + " " + _property.Type.Name + " " + _property.Name;
            }
        }

        public PropertyViewModel(Property property)
        {
            _property = property;
        }

        public override void LoadChildren()
        {
            Children.Clear();
            Children.Add(new TypeViewModel(_property.Type));
        }
    }
}
