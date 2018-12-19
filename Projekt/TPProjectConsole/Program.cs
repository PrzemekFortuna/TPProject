using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TPProject.ViewModel;
using TPProjectLib.ViewModel;

namespace TPProjectConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ReflectionViewModel viewModel = new ReflectionViewModel();
            SimpleIoc.Default.Register<ReflectionViewModel>();
            Compose(ViewModelLocator.ReflectionVM);
            Console.WriteLine("Provide absolute path to .dll file you want to inspect:");
            //ViewModelLocator.ReflectionVM.FileLoader = new ConsoleFileLoader();
            ViewModelLocator.ReflectionVM.LoadCommand.Execute(null);
            TreeViewModel treeViewModel = new TreeViewModel(ViewModelLocator.ReflectionVM.FileName);
            Console.WriteLine(treeViewModel.Output);

            while(true)
            {
                System.ConsoleKeyInfo key = Console.ReadKey();
                if(key.Key == ConsoleKey.Spacebar)
                {
                    treeViewModel.Expand();
                    Console.Clear();
                    Console.WriteLine(treeViewModel.Output);
                }
                if(key.Key == ConsoleKey.Backspace)
                {
                    treeViewModel.Shrink();
                    Console.Clear();
                    Console.WriteLine(treeViewModel.Output);
                }
            }
        }

        private static void Compose(object obj)
        {
            NameValueCollection plugins = (NameValueCollection)ConfigurationManager.GetSection("plugins");
            string[] pluginsCatalogs = plugins.AllKeys;
            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            foreach (string pluginsCatalog in pluginsCatalogs)
            {
                if (Directory.Exists(pluginsCatalog))
                    directoryCatalogs.Add(new DirectoryCatalog(pluginsCatalog));
            }

            AggregateCatalog catalog = new AggregateCatalog(directoryCatalogs);
            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(obj);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            catch (Exception exception) when (exception is ReflectionTypeLoadException)
            {
                ReflectionTypeLoadException typeLoadException = (ReflectionTypeLoadException)exception;
                Exception[] loaderExceptions = typeLoadException.LoaderExceptions;
                loaderExceptions.ToList().ForEach(ex => Console.WriteLine(ex.StackTrace));

                throw;
            }
        }
    }
}
