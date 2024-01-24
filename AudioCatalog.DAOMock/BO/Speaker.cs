using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.DAOMock.BO
{
    public class Speaker : ISpeaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public float Power { get; set; }
        public float Weight { get; set; }
        public ColorType Color { get; set; }
    }
}
