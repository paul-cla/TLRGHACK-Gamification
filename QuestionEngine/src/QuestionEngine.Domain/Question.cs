using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QuestionEngine.Domain
{
    [DataContract(Namespace = "http://api.laterooms.com/question/")]
    [JsonObject(MemberSerialization.OptIn)]
    public class Question
    {
        // ReSharper disable InconsistentNaming
        public Question(int ID, string Text)
            // ReSharper restore InconsistentNaming
        {
            Id = ID;
            this.Text = Text;
           // this.Answers = Answers;
        }

        [DataMember]
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id
        {
            get; private set;
        }

        [DataMember]
        [JsonProperty("Text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text
        {
            get; private set;
        }

        //[DataMember]
        //[JsonProperty("Answers", NullValueHandling = NullValueHandling.Ignore)]
        //public List<Answer> Answers
        //{
        //    get; private set;
        //}
    }
}