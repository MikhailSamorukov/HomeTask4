using SystemFileWatcher;

namespace FourthHomeTask
{
    class Program
    {
        public static void Main()
        {
            var fw = new FileWatcher();
            fw.Start();
        }
    }
}
