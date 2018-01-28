using System.Globalization;

namespace SystemFileWatcher.Abstract
{
    interface ICulturer
    {
      CultureInfo CurrentCulture { get; set; }
      void SetCulture();
    }
}
