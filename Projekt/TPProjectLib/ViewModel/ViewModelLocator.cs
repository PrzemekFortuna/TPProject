using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using ViewModels.ViewModel;

namespace TPProjectLib.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ReflectionViewModel>(true);
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