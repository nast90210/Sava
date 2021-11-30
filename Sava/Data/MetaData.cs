using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sava.Data
{
    
    public class Party
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("trackid")]
        public int Trackid { get; set; }

        [JsonProperty("starts")]
        public List<object> Starts { get; set; }

        [JsonProperty("durationsms")]
        public List<int> Durationsms { get; set; }

        [JsonProperty("segmentids")]
        public List<int> Segmentids { get; set; }

        [JsonProperty("segmentchannels")]
        public List<int> Segmentchannels { get; set; }
    }

    public class Segment
    {
        [JsonProperty("start")]
        public object Start { get; set; }

        [JsonProperty("ms")]
        public int Ms { get; set; }

        [JsonProperty("inum")]
        public object Inum { get; set; }

        [JsonProperty("otherinum")]
        public int Otherinum { get; set; }

        [JsonProperty("partyids")]
        public List<int> Partyids { get; set; }

        [JsonProperty("partystates")]
        public List<int> Partystates { get; set; }
    }

    public class Contact
    {
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("start")]
        public object Start { get; set; }

        [JsonProperty("ms")]
        public int Ms { get; set; }

        [JsonProperty("parties")]
        public List<Party> Parties { get; set; }

        [JsonProperty("segments")]
        public List<Segment> Segments { get; set; }

        [JsonProperty("screens")]
        public List<object> Screens { get; set; }

        [JsonProperty("tracks")]
        public int Tracks { get; set; }
    }
    
    public class Meta
    {
        [JsonProperty("columns")]
        public List<string> Description { get; set; }
        
        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("trackId")]
        public int TrackId { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }
    }

    public class MetaData
    {
        [JsonProperty("titles")]
        public List<string> Titles { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }

        [JsonProperty("rows")]
        public List<Meta> Data { get; set; }
    }


}