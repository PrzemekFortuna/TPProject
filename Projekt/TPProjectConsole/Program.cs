using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPProject.ViewModel;
using TPProjectLib.Utility.FileLoaders;

namespace TPProjectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Provide absolute path to .dll file you want to inspect:");
            ViewModelLocator.ReflectionVM.FileLoader = new ConsoleFileLoader();
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
    }
}
