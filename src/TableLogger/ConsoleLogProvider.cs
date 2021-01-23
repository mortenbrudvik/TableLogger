using System;

namespace TableLogger
{
    public class ConsoleLogProvider : ILogProvider
    {
        public void WriteLine(string message)
        {
            Console.Out.WriteLine(message);
        }
    }
}