using System;
using System.IO;
using System.Linq;
using messages = SystemFileWatcher.CommonResourses.Messages;
using SystemFileWatcher.Abstract;
using SystemFileWatcher.Interfaces;
using System.Text.RegularExpressions;
using SystemFileWatcher.EventArguments;

namespace SystemFileWatcher
{
    class FileWorker : IFileWorker
    {
        private readonly IFileWatcher _fileWatcher;
        private readonly ILogger _logger;
        private readonly IConfigurator _configurator;
        private readonly ICulturer _culturer;

        public FileWorker(IFileWatcher fileWatcher)
        {
            _fileWatcher = fileWatcher;
            _fileWatcher.FileFinded += OnFileFound;
            _culturer = new Culturer();
            _logger = Logger.getLogger();
            _configurator = new Configurator();
        }

        private void OnFileFound(object sender, FoundFileEventArgs e)
        {
            MoveFile(e.FullFileName);
        }

        public void MoveFile(string filePath)
        {
            try
            {
                var expectedFilePath = GetPathFromConfig(filePath);

                var directory = Path.GetDirectoryName(expectedFilePath);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (!File.Exists(filePath))
                {
                    using (File.Create(filePath)) { }
                }

                if (File.Exists(expectedFilePath))
                    File.Delete(expectedFilePath);

                File.Move(filePath, expectedFilePath);
                _logger.LogInfo(string.Format($"{filePath} {messages.MovedTo} {expectedFilePath}."));

            }
            catch (Exception e)
            {
                _logger.LogError(string.Format($"{messages.LoggerError}: {e}"));
            }
        }

        private string GetPathFromConfig(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            foreach (var item in _configurator.GetRules())
            {
                var regex = new Regex(item.Expression);
                var matches = regex.Matches(fileName);
                if (matches.Count > 0)
                {
                    if (item.AddDateTransfer)
                    {
                        fileName = $"DateTransfer_{DateTime.Now.ToString("dd_MM_yyyy")}_{fileName}";
                    }
                    if (item.AddSerialNumber)
                    {
                        var amountFiles = Directory.GetFiles(Path.GetDirectoryName(item.DestinationFolder)).Count()+1;
                        fileName = $"SerialNumber_{amountFiles}_{fileName}";
                    }
                    return item.DestinationFolder + fileName;
                }
            }
            return _configurator.GetDefaultFolder() + fileName;
        }
    }
}
