using System;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.WebApp.Models
{
    public class AddSpeakerViewModel
    {
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public float Power { get; set; }
        public float Weight { get; set; }
        public ColorType Color { get; set; }
        public IEnumerable<IProducer> AllProducers { get; set; } = new List<IProducer>();
        public ColorType[] AllColors = (ColorType[]) Enum.GetValues(typeof(ColorType));
    }
}
