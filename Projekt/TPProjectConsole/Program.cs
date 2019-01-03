using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight.Ioc;
using TPProjectLib.ViewModel;
using ViewModels.ViewModel;

namespace Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ReflectionViewModel viewModel = new ReflectionViewModel();
            SimpleIoc.Default.Register<ReflectionViewModel>();
            Compose(ViewModelLocator.ReflectionVM);
            System.Console.WriteLine("Provide absolute path to .dll file you want to inspect:");
            //ViewModelLocator.ReflectionVM.FileLoader = new ConsoleFileLoader();
            ViewModelLocator.ReflectionVM.LoadCommand.Execute(null);
            TreeViewModel treeViewModel = new TreeViewModel(ViewModelLocator.ReflectionVM.FileName);
            System.Console.WriteLine(treeViewModel.Output);

            while(true)
            {
                System.ConsoleKeyInfo key = System.Console.ReadKey();
                if(key.Key == ConsoleKey.Spacebar)
                {
                    treeViewModel.Expand();
                    System.Console.Clear();
                    System.Console.WriteLine(treeViewModel.Output);
                }
                if(key.Key == ConsoleKey.Backspace)
                {
                    treeViewModel.Shrink();
                    System.Console.Clear();
                    System.Console.WriteLine(treeViewModel.Output);
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
                System.Console.WriteLine(compositionException.ToString());
            }
            catch (Exception exception) when (exception is ReflectionTypeLoadException)
            {
                ReflectionTypeLoadException typeLoadException = (ReflectionTypeLoadException)exception;
                Exception[] loaderExceptions = typeLoadException.LoaderExceptions;
                loaderExceptions.ToList().ForEach(ex => System.Console.WriteLine(ex.StackTrace));

                throw;
            }
        }
    }
}
