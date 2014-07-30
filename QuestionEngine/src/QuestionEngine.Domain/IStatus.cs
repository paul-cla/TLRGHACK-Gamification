using System.Net;

namespace Keywords.Domain
{
    public interface IStatus
    {
        string Message { get; }
        HttpStatusCode Code { get; }
    }
}