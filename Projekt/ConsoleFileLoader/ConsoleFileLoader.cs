using System;
using System.ComponentModel.Composition;
using System.IO;

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
