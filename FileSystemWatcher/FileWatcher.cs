using SystemFileWatcher.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using messages = SystemFileWatcher.CommonResourses.Messages;
using SystemFileWatcher.EventArguments;
using SystemFileWatcher.Interfaces;

namespace SystemFileWatcher
{
    public class FileWatcher : IFileWatcher
    {
        private readonly ILogger _logger;
        private readonly IConfigurator _configurator;
        private readonly List<string> _folderForListening;
        private readonly ICulturer _culturer;
        private readonly IFileWorker _fileWorker;

        public event EventHandler<FoundFileEventArgs> FileFinded;

        public FileWatcher()
        {
            _culturer = new Culturer();
            _logger = Logger.getLogger();
            _configurator = new Configurator();
            _fileWorker = new FileWorker(this);
            _folderForListening = _configurator.GetWatherFolders().ToList();
        }

        public void StartWatch()
        {
            _culturer.SetCulture();
            Console.WriteLine($"{messages.Exit}");
            Watch(_folderForListening);
            Console.CancelKeyPress += Exit;
            while (true)
            {
                Console.ReadKey(true);
            }
        }

        public void OnFileFound(string fullFileName) {
            FileFinded?.Invoke(this, new FoundFileEventArgs() { FullFileName = fullFileName});
        }

        private void NewFileCreated(object sender, FileSystemEventArgs e)
        {
            var date = _culturer.GetLocalDateString(File.GetCreationTime(e.FullPath));
            _logger.LogInfo($"{messages.NewFile}: {e.Name}, {messages.DateCreation} {date}");
            OnFileFound(e.FullPath);
        }

        private void Watch(string folderName)
        {
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            var watcher = new FileSystemWatcher();
            watcher.Created += NewFileCreated;
            watcher.Path = folderName;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.EnableRaisingEvents = true;
        }

        private void Watch(IEnumerable<string> foldersNames)
        {
            foreach (var item in foldersNames)
                Watch(item);
        }

        private void Exit(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            Environment.Exit(0);
        }
    }
}
