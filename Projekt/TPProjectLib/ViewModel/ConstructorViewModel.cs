using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TPProjectLib.Reflection;

namespace TPProject.ViewModel
{
    public class ConstructorViewModel : TreeViewItemViewModel
    {
        private Method _ctor;
        public override string Name
        {
            get
            {
                string name = string.Empty;
                name += _ctor.Access.ToString() + " " + _ctor.Name + " (";
                foreach(Parameter param in _ctor.Parameters)
                {
                    name += param.ParamType.Name + " " + param.Name + ", ";
                }

                name.Remove(name.Length - 2, 2);
                name += ")";
                return name;
            }
        }

        public ConstructorViewModel(Method ctor)
        {
            _ctor = ctor;
            Children.Clear();
        }

        public override void LoadChildren() { }
    }
}
