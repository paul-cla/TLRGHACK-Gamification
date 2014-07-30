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
        public Question(int ID, string Text, List<Answer> Answers, List<Media> media)
            // ReSharper restore InconsistentNaming
        {
            Id = ID;
            this.Text = Text;
            this.Answers = Answers;
            Media = media;
        }

        [DataMember]
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id
        {
            get; set;
        }

        [DataMember]
        [JsonProperty("Text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text
        {
            get; private set;
        }

        [DataMember]
        [JsonProperty("Answers", NullValueHandling = NullValueHandling.Ignore)]
        public List<Answer> Answers
        {
            get;
            private set;
        }

        [DataMember]
        [JsonProperty("Media", NullValueHandling = NullValueHandling.Ignore)]
        public List<Media> Media { get; set; }

        [DataMember]
        [JsonProperty("NextQuestion", NullValueHandling = NullValueHandling.Ignore)]
        public int NextQuestion { get; set; }
    }

    public class Media
    {
        [DataMember]
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [DataMember]
        [JsonProperty("Url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        public Media(string type, string url)
        {
            Type = type;
            Url = url;
        }
    }
}