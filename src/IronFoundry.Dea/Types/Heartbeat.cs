namespace IronFoundry.Dea.Types
{
    using System;
    using IronFoundry.Dea.JsonConverters;
    using Newtonsoft.Json;

    public class Heartbeat : Message
    {
        public Heartbeat(Instance instance)
        {
            Partition      = instance.Partition;
            Droplet        = instance.DropletID;
            Version        = instance.Version;
            InstanceID     = instance.InstanceID;
            Index          = instance.InstanceIndex;
            State          = instance.State;
            StateTimestamp = instance.StateTimestamp;
        }

        [JsonProperty(PropertyName = "cc_partition")]
        public string Partition { get; private set; }
        
        [JsonProperty(PropertyName = "droplet")]
        public Guid Droplet { get; private set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; private set; }

        [JsonProperty(PropertyName = "instance"), JsonConverter(typeof(VcapGuidConverter))]
        public Guid InstanceID { get; private set; }

        [JsonProperty(PropertyName = "index")]
        public uint Index { get; private set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; private set; }

        [JsonProperty(PropertyName = "state_timestamp")]
        public int StateTimestamp { get; private set; }
    }
}