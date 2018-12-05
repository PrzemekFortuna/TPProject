using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Utility
{
    public static class LogManager
    {
        [Import(typeof(ILogger))]
        public static ILogger Logger { get; set; }

        static LogManager()
        {
            
        }

        public static void Log(LogMode mode ,string message)
        {
            Logger.Log(mode, message);
        }
    }
}
