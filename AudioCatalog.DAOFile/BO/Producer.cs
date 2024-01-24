using System.Runtime.Serialization;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.DAOFile.BO
{
    [DataContract]
    public class Producer : IProducer
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string CountryOfOrigin { get; set; }

        [DataMember]
        public string Website { get; set; }
    }
}
