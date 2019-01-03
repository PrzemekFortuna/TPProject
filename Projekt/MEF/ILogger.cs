namespace MEF
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
