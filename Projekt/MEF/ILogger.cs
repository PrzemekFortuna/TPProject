using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Utility
{
    public enum LogMode
    {
        Warning,
        Critical,
        Info
    }
    public interface ILogger
    {
        void Log(LogMode mode ,string message);
    }
}
