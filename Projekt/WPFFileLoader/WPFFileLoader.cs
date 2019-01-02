using System.ComponentModel.Composition;
using MEF;
using Microsoft.Win32;

namespace WPFFileLoader
{
    [Export(typeof(IFileLoader))]
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
