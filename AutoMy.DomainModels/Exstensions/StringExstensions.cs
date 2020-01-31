using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.DomainModels.Exstensions
{
    public static class StringExstensions
    {
        public static bool IsAnything (this string text)
        {
            if (!String.IsNullOrEmpty(text) && !String.IsNullOrWhiteSpace(text))
                return true;
            else
                return false;
        }
    }
}
