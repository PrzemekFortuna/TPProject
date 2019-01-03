using DataLayer.Reflection;

namespace ViewModels.ViewModel
{
    public class NamespaceViewModel : TreeViewItemViewModel
    {
        private Namespace _namespace;
        public override string Name { get { return _namespace.Name; } }

        public NamespaceViewModel(Namespace ns)
        {
            _namespace = ns;
        }

        public override void LoadChildren()
        {
            Children.Clear();
            foreach(ReflectedType type in _namespace.Classes)
            {
                Children.Add(new TypeViewModel(type));
            }
            foreach(ReflectedType type in _namespace.ValueTypes)
            {
                Children.Add(new TypeViewModel(type));
            }
            foreach(ReflectedType type in _namespace.Interfaces)
            {
                Children.Add(new TypeViewModel(type));
            }
            WasBuild = true;
        }
    }
}
