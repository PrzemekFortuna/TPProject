using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Utility.FileLoaders
{
    public class WPFFileLoader : IFileLoader
    {
        public string LoadFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Reflectable types|*.exe;*.dll";
            fileDialog.ShowDialog();

            return (fileDialog.FileName.Length != 0) ? fileDialog.FileName : string.Empty;
        }
    }
}
