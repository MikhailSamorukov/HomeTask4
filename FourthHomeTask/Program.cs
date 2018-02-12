using System;
using SystemFileWatcher;

namespace FourthHomeTask
{
    class Program
    {
        public static void Main()
        {

            var fw = new FileWatcher();
            fw.StartWatch();
            Console.CancelKeyPress += Exit;
            while (true)
            {
                Console.ReadKey(true);
            }
        }
        private static void Exit(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            Environment.Exit(0);
        }
    }
}