using DataLayer.Reflection;

namespace ViewModels.ViewModel
{
    public class TypeViewModel : TreeViewItemViewModel
    {
        private ReflectedType _type;
        public override string Name { get { return ((_type.IsAbstract == true && _type.IsStatic == false) ? "abstract " : "") + ((_type.IsStatic == true) ? "static " : "") + _type.TypeKind.ToString() + " " + _type.Name; } }

        public TypeViewModel(ReflectedType type)
        {
            _type = type;
        }

        public override void LoadChildren()
        {
            Children.Clear();
            
            if(_type.Fields != null)
            {
                foreach(Field field in _type.Fields)
                {
                    Children.Add(new FieldViewModel(field));
                }
            }

            if(_type.Properties != null)
            {
                foreach (Property property in _type.Properties)
                {
                    Children.Add(new PropertyViewModel(property));
                }
            }

            if(_type.Constructors != null)
            {
                foreach (Method ctor in _type.Constructors)
                {
                    Children.Add(new ConstructorViewModel(ctor));
                }
            }

            if(_type.Methods != null)
            {
                foreach (Method method in _type.Methods)
                {
                    Children.Add(new MethodViewModel(method));
                }
            }
            WasBuild = true;
        }
    }
}
