using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TPProject
{
    public static class Program
    {

        [STAThread]
        public static int Main(string[] args)
        {
                Console.WriteLine("Hello, press enter to enter GUI mode");
                Console.ReadLine();
                App.Main();
                return 0;
        }
    }
}
