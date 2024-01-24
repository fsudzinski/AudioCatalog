using Sudzinski.AudioCatalog.Core;

namespace Sudzinski.AudioCatalog.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<ISpeaker> GetAllSpeakers();
        
        void CreateProducer(string name, string countryOfOrigin, string website);
        void CreateSpeaker(string name, int producerId, float power, float weight, ColorType color);
        void UpdateProducer(int id, string name, string countryOfOrigin, string website);
        void UpdateSpeaker(int id, string name, int producerId, float power, float weight, ColorType color);
        void DeleteProducer(int id);
        void DeleteSpeaker(int id);
        IProducer? GetProducerById(int id);
        ISpeaker? GetSpeakerById(int id);
    }
}
