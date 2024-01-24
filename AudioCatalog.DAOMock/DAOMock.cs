using System;
using System.Collections.Generic;
using System.Linq;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.DAOMock.BO;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.DAOMock
{
    public class DAOMock : IDAO
    {
        private List<Producer> Producers;
        private List<Speaker> Speakers;

        public int GenerateUniqueId<T>(IEnumerable<T> items, Func<T, int> getId)
        {
            if (items == null)
            {
                return 1;
            }

            var existingIds = items.Select(getId);

            int minUnusedId = 1;
            while (existingIds.Contains(minUnusedId))
            {
                minUnusedId++;
            }

            return minUnusedId;
        }

        public DAOMock()
        {
            Producers = new List<Producer>()
            {
                new Producer()
                {
                    Id = 1,
                    Name = "JBL",
                    CountryOfOrigin = "USA",
                    Website = "https://www.jbl.com"
                },
                new Producer()
                {
                    Id = 2,
                    Name = "Sony",
                    CountryOfOrigin = "Japan",
                    Website = "https://www.sony.com"
                }
            };

            Speakers = new List<Speaker>()
            {
                new Speaker()
                {
                    Id = 1,
                    Name = "JBL Charge 4",
                    Producer = Producers[0],
                    Power = 30,
                    Weight = 0.96f,
                    Color = ColorType.Blue
                },
                new Speaker()
                {
                    Id = 2,
                    Name = "Sony SRS-XB12",
                    Producer = Producers[1],
                    Power = 10,
                    Weight = 0.25f,
                    Color = ColorType.Green
                }
            };

        }

        public void CreateProducer(string name, string countryOfOrigin, string website)
        {
            Producer producer = new Producer()
            {
                Id = GenerateUniqueId(Producers, producer => producer.Id),
                CountryOfOrigin = countryOfOrigin,
                Name = name,
                Website = website,
                Speakers = new List<ISpeaker>()
            };
            Producers.Add(producer);
        }

        public void CreateSpeaker(string name, int producerId, float power, float weight, ColorType color)
        {
            IProducer producer = GetProducerById(producerId);
            Speaker speaker = new Speaker()
            {
                Id = GenerateUniqueId(Speakers, s => s.Id),
                Name = name,
                Producer = producer,
                Power = power,
                Weight = weight,
                Color = color
            };
            Speakers.Add(speaker);
            producer.Speakers.Add(speaker);
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return Producers;
        }

        public IEnumerable<ISpeaker> GetAllSpeakers()
        {
            return Speakers;
        }

        public IProducer? GetProducerById(int id)
        {
            return Producers.First(p => p.Id == id);
        }

        public ISpeaker? GetSpeakerById(int id)
        {
            return Speakers.First(s => s.Id == id);
        }

        public void UpdateProducer(int id, string name, string countryOfOrigin, string website)
        {
            IProducer producer = GetProducerById(id);            
            producer.Name = name;
            producer.CountryOfOrigin = countryOfOrigin;
            producer.Website = website;            
        }

        public void UpdateSpeaker(int id, string name, int producerId, float power, float weight, ColorType color)
        {
            ISpeaker existingSpeaker = GetSpeakerById(id);
            
            if(existingSpeaker.Producer.Id != producerId)
            {
                existingSpeaker.Producer.Speakers.Remove(existingSpeaker);
                GetProducerById(producerId).Speakers.Add(existingSpeaker);
            }
                
            existingSpeaker.Name = name;
            existingSpeaker.Producer = GetProducerById(producerId);
            existingSpeaker.Power = power;
            existingSpeaker.Weight = weight;
            existingSpeaker.Color = color;
            
        }

        public void DeleteProducer(int id)
        {
            IProducer producer = GetProducerById(id);
            Speakers.RemoveAll(s => s.Producer.Id == producer.Id);
            Producers.Remove((Producer)producer);
        }

        public void DeleteSpeaker(int id)
        {
            ISpeaker speaker = GetSpeakerById(id);
            Speakers.Remove((Speaker)speaker);
        }

    }
}
