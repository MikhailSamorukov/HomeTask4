using SystemFileWatcher.Abstract;
using System;
using System.Collections.Generic;

namespace SystemFileWatcher
{
    class Logger : ILogger
    {
        private readonly List<string> _privateJournal;

        public IEnumerable<string> Journal => _privateJournal;

        public Logger()
        {
            _privateJournal = new List<string>();
        }
        public void AddLog(string information)
        {
            _privateJournal.Add(information);
            Console.WriteLine(information);
        }
    }

    class LoggerEventArgs : EventArgs {
        public string Information { get; set; }
    }
}
