using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.WebApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<IProducer> Producers { get; set; }
        public IEnumerable<ISpeaker> Speakers { get; set; }
        public string ProducersSearchTerm { get; set; }
        public string SpeakersSearchTerm { get; set; }
        public List<string> AllCountries { get; set; }
        public ColorType[] AllColors = (ColorType[])Enum.GetValues(typeof(ColorType));
    }
}
