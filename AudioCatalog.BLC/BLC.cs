using System.Reflection;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.BLC
{
    public class BLC
    {
        private IDAO dao;

        public BLC(string libraryName)
        {
            Type? typeToCreate = null;

            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = type;
                    break;
                }
            }

            dao = (IDAO)Activator.CreateInstance(typeToCreate, null);
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return dao.GetAllProducers();
        }

        public IEnumerable<ISpeaker> GetAllSpeakers()
        {
            return dao.GetAllSpeakers();
        }

        public void CreateProducer(string name, string countryOfOrigin, string website)
        {
            dao.CreateProducer(name, countryOfOrigin, website);
        }
        public void CreateSpeaker(string name, int producerId, float power, float weight, ColorType color)
        {
            dao.CreateSpeaker(name, producerId, power, weight, color);
        }
        public void UpdateProducer(int id, string name, string countryOfOrigin, string website)
        {
            dao.UpdateProducer(id, name, countryOfOrigin, website);
        }
        public void UpdateSpeaker(int id, string name, int producerId, float power, float weight, ColorType color)
        {
            dao.UpdateSpeaker(id, name, producerId, power, weight, color);
        }
        public void DeleteProducer(int id)
        {
            dao.DeleteProducer(id);
        }
        public void DeleteSpeaker(int id)
        {
            dao.DeleteSpeaker(id);
        }
        public IProducer? GetProducerById(int id)
        {
            return dao.GetProducerById(id);
        }
        public ISpeaker? GetSpeakerById(int id)
        {
            return dao.GetSpeakerById(id);
        }

    }
}
