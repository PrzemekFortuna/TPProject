using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TPProjectLib.Reflection;

namespace TPProject.ViewModel
{
    public class MethodViewModel : TreeViewItemViewModel
    {
        private Method _method;
        public override string Name
        {
            get
            {
                string name = string.Empty;
                name += _method.Access.ToString() + " " + _method.ReturnType.Name + " " + _method.Name + "( ";

                foreach (Parameter param in _method.Parameters)
                {
                    name += param.ParamType.Name + " " + param.Name + ", ";
                }

                if(_method.Parameters.Count != 0)
                    name = name.Remove(name.Length - 2);
                name += ")";
                return name;
            }
        }

        public MethodViewModel(Method method)
        {
            _method = method;
            
        }

        public override void LoadChildren()
        {
            Children.Clear();
            Children.Add(new TypeViewModel(_method.ReturnType));
            WasBuild = true;
        }
    }
}
