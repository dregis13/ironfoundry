namespace IronFoundry.Dea.Types
{
    using System;
    using Newtonsoft.Json;

    public class Discover : EntityBase
    {
        [JsonProperty(PropertyName = "droplet")]
        public Guid DropletID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "runtime")]
        public string Runtime { get; set; }

        [JsonProperty(PropertyName = "sha")]
        public string Sha { get; set; }

        [JsonProperty(PropertyName = "limits")]
        public Limits Limits { get; set; }
    }
}