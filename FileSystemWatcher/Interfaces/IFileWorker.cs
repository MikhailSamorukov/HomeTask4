using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemFileWatcher.Interfaces
{
    interface IFileWorker
    {
        void MoveFile(string filePath);
    }
}
