using System;

namespace Keywords.API.Support
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}