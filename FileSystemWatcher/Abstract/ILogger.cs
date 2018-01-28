using System.Collections.Generic;

namespace SystemFileWatcher.Abstract
{
    interface ILogger
    {
        void AddLog(string information);
        IEnumerable<string> Journal { get; }
    }
}
