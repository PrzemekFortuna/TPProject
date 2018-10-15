using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"E:\Uczelnia\TPA\TPProject\Projekt\ExampleDLL\bin\Debug\ExampleDLL.dll";
            TreeViewModel treeViewModel = new TreeViewModel(url);
            Console.WriteLine(treeViewModel.Output);

            while(true)
            {
                var key = Console.ReadKey();
                if(key.Key == ConsoleKey.Spacebar)
                {
                    treeViewModel.Expand();
                    Console.Clear();
                    Console.WriteLine(treeViewModel.Output);
                }
            }
        }
    }
}
