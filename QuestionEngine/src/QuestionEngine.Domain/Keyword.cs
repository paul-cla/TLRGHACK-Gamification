using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Keywords.Domain
{
    [DataContract(Namespace = "http://api.laterooms.com/keyword/")]
    [JsonObject(MemberSerialization.OptIn)]
    public class Keyword
    {
        // ReSharper disable InconsistentNaming
        public Keyword(int KeywordID, string KeywordText, string AreaName, string Country, int AreaID, string FriendlyText)
        // ReSharper restore InconsistentNaming
        {
            Id = KeywordID;
            this.KeywordText = KeywordText;
            this.AreaName = AreaName;
            AreaId = AreaID;
            this.Country = Country;
            this.FriendlyText = FriendlyText;
        }

        [DataMember]
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id
        {
            get; private set;
        }

        [DataMember]
        [JsonProperty("KeywordText", NullValueHandling = NullValueHandling.Ignore)]
        public string KeywordText
        {
            get; private set;
        }

        [DataMember]
        [JsonProperty("AreaName", NullValueHandling = NullValueHandling.Ignore)]
        public string AreaName
        {
            get; private set;
        }

        [DataMember]
        [JsonProperty("Country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country
        {
            get; private set;
        }

        [DataMember]
        [JsonProperty("AreaID", NullValueHandling = NullValueHandling.Ignore)]
        public int AreaId
        {
            get; private set;
        }
        
        [DataMember]
        [JsonProperty("FriendlyText", NullValueHandling = NullValueHandling.Ignore)]
        public string FriendlyText
        {
            get;
            private set;
        }
    }
}
