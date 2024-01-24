using System.Runtime.Serialization;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.DAOFile.BO
{
    [DataContract]
    public class Speaker : ISpeaker
    {
        [DataMember]
        public int Id { get; set;  }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IProducer Producer { get; set; }

        [DataMember]
        public float Power { get; set; }

        [DataMember]
        public int FrequencyRange { get; set; }

        [DataMember]
        public float Weight { get; set; }

        [DataMember]
        public float MaxBatteryLife { get; set; }

        [DataMember]
        public float ChargingTime { get; set; }

        [DataMember]
        public WirelessType WirelessType { get; set; }

    }
}
