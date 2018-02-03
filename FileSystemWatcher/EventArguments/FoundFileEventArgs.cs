using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemFileWatcher.EventArguments
{
    public class FoundFileEventArgs:EventArgs
    {
        public string FullFileName { get; set; }
    }
}
