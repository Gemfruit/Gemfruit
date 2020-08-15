using System;

namespace Gemfruit.Mod.Internal
{
    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }
    
    internal class Logger
    {
        public LogLevel MaskLevel { get; set; }

        public Logger(LogLevel maskLevel = LogLevel.Debug)
        {
            MaskLevel = maskLevel;
        }

        public void Log(LogLevel level, string location, string text)
        {
            if (level <= MaskLevel) return;
            if(level <= LogLevel.Warning)
                Console.WriteLine($"[{level}] {{{location}}} {text}");
            else
                Console.Error.WriteLine($"[{level}] {{{location}}} {text}");
        }
    }
}