using MEF;

namespace ViewModels.Utility
{
    public static class LogManager
    {
        
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
