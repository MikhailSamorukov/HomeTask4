using SystemFileWatcher.Abstract;
using System;
using System.Globalization;
using messages = SystemFileWatcher.CommonResourses.Messages;

namespace SystemFileWatcher
{
    class Culturer : ICulturer
    {
        IConfigurator _configurator;
        public Culturer()
        {
            _configurator = new Configurator();
        }

        public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        public string GetLocalDateString(DateTime date) {
            return date.ToString(CurrentCulture.DateTimeFormat);
        }

        public void SetCulture() {
            var isCultureSelected = false;
            Console.WriteLine(messages.StartMessage);
            while (!isCultureSelected)
            {
                if (Console.ReadKey().Key == ConsoleKey.R)
                {
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(_configurator.GetCulture("Russian"));
                    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(_configurator.GetCulture("Russian"));
                }
                else
                {
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(_configurator.GetCulture("English"));
                    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(_configurator.GetCulture("English"));
                }
                isCultureSelected = true;
            }
        }
    }
}
