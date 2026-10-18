using System;
using System.IO;

namespace EduHub.Ch02.Services
{
    public class FileAppLogger : IAppLogger
    {
        private readonly string _logFilePath = "club-log.txt";

        public void Log(string message)
        {
            File.AppendAllText(_logFilePath, $"[{DateTime.Now}] {message}\n");
        }
    }
}