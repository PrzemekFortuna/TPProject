using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TPProjectLib.Reflection;

namespace TPProject.ViewModel
{
    public class FieldViewModel : TreeViewItemViewModel
    {
        private Field _field;
        public override string Name
        {
            get
            {
                return _field.Access.ToString() + " " + _field.Type.Name + " " + _field.Name;
            }
        }

        public FieldViewModel(Field field)
        {
            _field = field;
        }

        public override void LoadChildren()
        {
            Children.Clear();
            Children.Add(new TypeViewModel(_field.Type));
            WasBuild = true;
        }
    }
}
