using System;

namespace Keywords.API.Support
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}