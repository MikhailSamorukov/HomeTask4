using SystemFileWatcher.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using SystemFileWatcher.ConfigItems;
using messages = SystemFileWatcher.CommonResourses.Messages;
using System.Threading;

namespace SystemFileWatcher
{
    public class FileWatcher : IFileWatcher
    {
        private readonly List<FileSystemWatcher> _watchers;
        private readonly ILogger _logger;
        private readonly IConfigurator _configurator;
        private readonly List<string> _folderForListening;
        private readonly ICulturer _culturer;

        public FileWatcher()
        {
            _culturer = new Culturer();
            _logger = new Logger();
            _configurator = new Configurator();
            _watchers = new List<FileSystemWatcher>();
            _folderForListening = _configurator.GetWatherFolders().ToList();
        }

        private void _watcher_Created(object sender, FileSystemEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = _culturer.CurrentCulture;
            _logger.AddLog($"{messages.NewFile}: {e.Name}, {messages.DateCreation} {DateTime.Now.ToString(_culturer.CurrentCulture.DateTimeFormat)}");
            MoveTo(e.FullPath, GetPathFromConfig(e.Name));
        }

        private string GetPathFromConfig(string fileName) {
           
            foreach (var item in _configurator.GetRules())
            {
                var regex = new Regex(item.Expression);
                var matches = regex.Matches(fileName);
                if (matches.Count > 0)
                    return item.DestinationFolder + fileName;
            }
            return _configurator.GetDefaultFolder() + fileName;
        }

        private void Listen(string folderName)
        {
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            var watcher = new FileSystemWatcher();
            watcher.Created += _watcher_Created;
            watcher.Path = folderName;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.EnableRaisingEvents = true;
            _watchers.Add(watcher);
        }

        private void Listen(IEnumerable<string> foldersNames)
        {
            foreach (var item in foldersNames)
                Listen(item);
        }

        private void MoveTo(string originalFilePath, string expectedFilePath)
        {
            try
            {
                var directory = Path.GetDirectoryName(expectedFilePath);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (!File.Exists(originalFilePath))
                {
                    using (File.Create(originalFilePath))
                    { }
                }

                if (File.Exists(expectedFilePath))
                    File.Delete(expectedFilePath);

                File.Move(originalFilePath, expectedFilePath);
                _logger.AddLog(string.Format($"{originalFilePath} {messages.MovedTo} {expectedFilePath}."));

            }
            catch (Exception e)
            {
                _logger.AddLog(string.Format($"{messages.LoggerError}: {e}"));
            }
        }

        public void Start()
        {
            _culturer.SetCulture();
            Console.WriteLine($"{messages.Exit}");
            Listen(_folderForListening);
            Console.CancelKeyPress += Exit;
            while (true)
            {
                Console.ReadKey(true);
            }
        }

        public void WriteLog()
        {
            try
            {
                if (!File.Exists("D:\\log.txt"))
                    File.Create("D:\\log.txt");

                using (var sw = new StreamWriter($"D:\\log.txt", false, Encoding.Default))
                {
                    foreach (var item in _logger.Journal)
                    {
                        sw.WriteLine(item);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.AddLog(string.Format($"{messages.LoggerError}: {e}"));
            }
        }

        private void Exit(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            WriteLog();
            Environment.Exit(0);
        }
    }
}
