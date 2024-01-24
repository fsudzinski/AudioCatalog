using Sudzinski.AudioCatalog.Core;

namespace Sudzinski.AudioCatalog.Interfaces
{
    public interface ISpeaker
    {
        int Id { get; set; }
        string Name { get; set; }
        IProducer Producer { get; set; }
        float Power { get; set; }
        float Weight { get; set; }   
        ColorType Color { get; set; }

    }
}
