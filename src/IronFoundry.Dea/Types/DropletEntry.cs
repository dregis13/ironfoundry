namespace IronFoundry.Dea.Types
{
    using System;
    using Newtonsoft.Json;

    public class DropletEntry : EntityBase
    {
        [JsonProperty(PropertyName = "droplet")]
        public Guid DropletID { get; set; }

        [JsonProperty(PropertyName = "instances")]
        public InstanceEntry[] Instances;
    }
}