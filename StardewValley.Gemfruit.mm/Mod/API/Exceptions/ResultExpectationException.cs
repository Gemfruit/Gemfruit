using System;

namespace Gemfruit.Mod.API.Exceptions
{
    public class ResultExpectationException : Exception
    {
        public ResultExpectationException(string message, string error) : base($"{message}: {error}")
        {
            
        }
    }
}