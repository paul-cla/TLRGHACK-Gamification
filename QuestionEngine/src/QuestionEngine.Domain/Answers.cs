using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QuestionEngine.Domain
{
    [DataContract(Namespace = "http://api.laterooms.com/question/")]
    [JsonObject(MemberSerialization.OptIn)]
    public class Answers
    {

    }
}