using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Utility.FileLoaders
{
    [Export(typeof(IFileLoader))]
    public class ConsoleFileLoader : IFileLoader
    {
        public string LoadFile()
        {
            string path = Console.ReadLine();

            if (path != null && File.Exists(path) && (path.Contains(".dll")))
            {
                return path;
            }
            Console.WriteLine("Invalid path");
            return null;
        }
    }
}
