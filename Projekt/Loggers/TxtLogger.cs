using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using MEF;

namespace TxtLogger
{
    [Export(typeof(ILogger))]
    public class TxtLogger : ILogger
    {
        private string _fileName;

        public TxtLogger()
        {
            _fileName = "log.txt";

            if (File.Exists(_fileName))
            {
                
            }
        }

        public void Log(LogMode mode ,string message)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("[{0}]\t", mode.ToString());
            sb.AppendFormat("{0}{1}", message, Environment.NewLine);

            File.AppendAllText(_fileName, sb.ToString());   
        }
    }
}
