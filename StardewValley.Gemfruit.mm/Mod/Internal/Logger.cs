using System;

namespace Gemfruit.Mod.Internal
{
    public enum LogLevel
    {
        TRACE,
        DEBUG,
        INFO,
        WARNING,
        ERROR,
        FATAL
    }
    
    public class Logger
    {
        public LogLevel MaskLevel { get; set; }

        public Logger(LogLevel maskLevel = LogLevel.DEBUG)
        {
            MaskLevel = maskLevel;
        }

        public void Log(LogLevel level, string location, string text)
        {
            if (level > MaskLevel)
            {
                if(level <= LogLevel.WARNING)
                    Console.WriteLine($"[{level}] {{{location}}} {text}");
                else
                    Console.Error.WriteLine($"[{level}] {{{location}}} {text}");
            }
        }
    }
}