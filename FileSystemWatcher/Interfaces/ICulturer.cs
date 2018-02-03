using System;
using System.Globalization;

namespace SystemFileWatcher.Abstract
{
    interface ICulturer
    {
      CultureInfo CurrentCulture { get; }
      void SetCulture();
      string GetLocalDateString(DateTime date);
    }
}
