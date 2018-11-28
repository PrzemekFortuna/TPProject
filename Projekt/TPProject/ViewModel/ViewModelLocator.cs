using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace TPProject.ViewModel
{
    public static class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ReflectionViewModel>();
        }

        public static MainViewModel MainVM
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainViewModel>();
            }
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