using System.Collections.Generic;

namespace SystemFileWatcher.Abstract
{
    interface ILogger
    {
        void LogInfo(string information);
        void LogError(string errorDetails);
    }
}
