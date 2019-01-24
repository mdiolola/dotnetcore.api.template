using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetcore.api.model.Common
{
    public sealed class DateTimeHelper
    {
        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public static DateTime GetDateTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
