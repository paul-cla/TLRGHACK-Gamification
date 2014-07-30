using System.Net;

namespace Keywords.Domain
{
    public class GoodStatus : IStatus
    {
        public string Message
        {
            get { return "OK"; }
        }

        public HttpStatusCode Code
        {
            get { return HttpStatusCode.OK; }
        }
    }
}