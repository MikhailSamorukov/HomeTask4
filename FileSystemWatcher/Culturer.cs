using SystemFileWatcher.Abstract;
using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using SystemFileWatcher.ConfigItems;

namespace SystemFileWatcher
{
    class Culturer : ICulturer
    {
        IConfigurator _configurator;
        public Culturer()
        {
            _configurator = new Configurator();
        }
        public CultureInfo CurrentCulture { get; set; }
        public void SetCulture() {
            var keyCulture = true;
            Console.WriteLine("select culture key r - Russian, other English/Выберите культуру, кнопка к - Русская, остальные Английская");
            while (keyCulture)
            {
                if (Console.ReadKey().Key == ConsoleKey.R)
                {
                    Thread.CurrentThread.CurrentUICulture = CurrentCulture = new CultureInfo(_configurator.GetCulture("Russian"));
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = CurrentCulture = new CultureInfo(_configurator.GetCulture("English"));
                }
                keyCulture = false;
            }
        }
    }
}
