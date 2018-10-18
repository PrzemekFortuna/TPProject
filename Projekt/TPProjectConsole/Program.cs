using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultUrl = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))) + @"\ExampleDLL\bin\Debug\ExampleDLL.dll";
            Console.WriteLine("Provide absolute path to .dll file you want to inspect (default is " + defaultUrl +"):");
            string url = @Console.ReadLine();
            if (url.Length == 0)
            {
                url = defaultUrl;
            }
            TreeViewModel treeViewModel = new TreeViewModel(url);
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
