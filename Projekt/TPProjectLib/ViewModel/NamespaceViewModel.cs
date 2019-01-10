using BusinessLogic.Reflection;

namespace ViewModels.ViewModel
{
    public class NamespaceViewModel : TreeViewItemViewModel
    {
        public Namespace _namespace { get; }
        public override string Name { get { return _namespace.Name; } }

        public NamespaceViewModel(Namespace ns)
        {
            _namespace = ns;
        }

        public NamespaceViewModel(NamespaceViewModel nsvm)
        {
            _namespace = nsvm._namespace;
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
