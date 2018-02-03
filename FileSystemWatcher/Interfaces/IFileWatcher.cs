using System;
using SystemFileWatcher.EventArguments;

namespace SystemFileWatcher.Abstract
{
    public interface IFileWatcher
    {
        void StartWatch();
        event EventHandler<FoundFileEventArgs> FileFinded;
    }
}
