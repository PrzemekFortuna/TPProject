using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Utility
{
    public static class LogManager
    {
        public static ILogger Logger { get; set; }

        static LogManager()
        {
            Logger = new TxtLogger();
        }

        public static void Log(LogMode mode ,string message)
        {
            Logger.Log(mode, message);
        }
    }
}
