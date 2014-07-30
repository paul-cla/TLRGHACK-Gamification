using System;

namespace QuestionEngine.API.Support
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}