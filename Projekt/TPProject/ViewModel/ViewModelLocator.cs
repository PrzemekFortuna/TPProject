using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace TPProject.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ReflectionViewModel>();
        }

        public static ReflectionViewModel ReflectionVM
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ReflectionViewModel>();
            }
        }
    }
}