using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.DAOMock.BO
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Website { get; set; }
        public List<ISpeaker> Speakers { get; set; } = new List<ISpeaker>();
    }
}
