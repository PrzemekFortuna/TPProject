using BusinessLogic.Reflection;

namespace ViewModels.ViewModel
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
            WasBuild = true;
        }
    }
}
