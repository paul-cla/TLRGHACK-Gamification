using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QuestionEngine.Domain
{
    [DataContract(Namespace = "http://api.laterooms.com/keyword/")]
    [JsonObject(MemberSerialization.OptIn)]
    public class Region
    {
        // ReSharper disable InconsistentNaming
        public Region(int RegionId, string RegionText, string FriendlyText)
        // ReSharper restore InconsistentNaming
        {
            Id = RegionId;
            this.RegionText = RegionText;
            this.FriendlyText = FriendlyText;
        }

        [DataMember]
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id
        {
            get; private set;
        }

        [DataMember]
        [JsonProperty("RegionText", NullValueHandling = NullValueHandling.Ignore)]
        public string RegionText
        {
            get; private set;
        }

        [DataMember]
        [JsonProperty("FriendlyText", NullValueHandling = NullValueHandling.Ignore)]
        public string FriendlyText
        {
            get; private set;
        }
    }
}
