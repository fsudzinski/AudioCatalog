using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.DAOFile.BO;
using Sudzinski.AudioCatalog.Interfaces;
using System.Runtime.Serialization;

namespace Sudzinski.AudioCatalog.DAOFile
{
    public class DAOFile : IDAO
    {
        private const string ProducersFileName = "Producers.xml";
        private const string SpeakersFileName = "Speakers.xml";

        public DAOFile()
        {
            

            if (!File.Exists(ProducersFileName) && !File.Exists(SpeakersFileName))
            {
                var producers = new List<Producer>
                {
                    new Producer
                    {
                        Id = 1,
                        Name = "JBL",
                        CountryOfOrigin = "USA",
                        Website = "https://www.jbl.com.pl/"
                    },
                    new Producer
                    {
                        Id = 2,
                        Name = "Sony",
                        CountryOfOrigin = "Japan",
                        Website = "https://www.sony.pl/"
                    },
                    new Producer
                    {
                        Id = 3,
                        Name = "Bose",
                        CountryOfOrigin = "USA",
                        Website = "https://www.bose.pl/"
                    }
                };

                using (var streamWriter = new StreamWriter(ProducersFileName))
                {
                    var serializer = new DataContractSerializer(typeof(List<IProducer>));
                    serializer.WriteObject(streamWriter.BaseStream, producers);
                }

                var speakers = new List<Speaker>
                {
                    new Speaker
                    {
                        Id = 1,
                        Name = "JBL Charge 4",
                        Producer = producers[0],
                        Power = 30,
                        FrequencyRange = 60,
                        Weight = 0.96f,
                        MaxBatteryLife = 20,
                        ChargingTime = 4,
                        WirelessType = WirelessType.Bluetooth
                    },
                    new Speaker
                    {
                        Id = 2,
                        Name = "JBL Flip 5",
                        Producer = producers[1],
                        Power = 20,
                        FrequencyRange = 65,
                        Weight = 0.54f,
                        MaxBatteryLife = 12,
                        ChargingTime = 2.5f,
                        WirelessType = WirelessType.Bluetooth
                    },
                    new Speaker
                    {
                        Id = 3,
                        Name = "Sony SRS-XB12",
                        Producer = producers[2],
                        Power = 20,
                        FrequencyRange = 65,
                        Weight = 0.54f,
                        MaxBatteryLife = 12,
                        ChargingTime = 2.5f,
                        WirelessType = WirelessType.Bluetooth
                    }
                };

                using (var streamWriter = new StreamWriter(SpeakersFileName))
                {
                    var serializer = new DataContractSerializer(typeof(List<ISpeaker>));
                    serializer.WriteObject(streamWriter.BaseStream, speakers);
                }
            }
        }

        public void CreateProducer(IProducer producer)
        {
            var producers = GetAllProducers().ToList();
            producers.Add(producer);

            using (var streamWriter = new StreamWriter(ProducersFileName))
            {
                var serializer = new DataContractSerializer(typeof(List<IProducer>));
                serializer.WriteObject(streamWriter.BaseStream, producers);
            }
        }

        public void CreateSpeaker(ISpeaker speaker)
        {
            var speakers = GetAllSpeakers().ToList();
            speakers.Add(speaker);

            using (var streamWriter = new StreamWriter(SpeakersFileName))
            {
                var serializer = new DataContractSerializer(typeof(List<ISpeaker>));
                serializer.WriteObject(streamWriter.BaseStream, speakers);
            }
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            List<IProducer> producers;

            if (File.Exists(ProducersFileName))
            {
                using (var streamReader = new StreamReader(ProducersFileName))
                {
                    var serializer = new DataContractSerializer(typeof(List<IProducer>));
                    producers = (List<IProducer>)serializer.ReadObject(streamReader.BaseStream);
                }
            }
            else
            {
                producers = new List<IProducer>();
            }

            return producers;
        }

        public IEnumerable<ISpeaker> GetAllSpeakers()
        {
            List<ISpeaker> speakers;

            if (File.Exists(SpeakersFileName))
            {
                using (var streamReader = new StreamReader(SpeakersFileName))
                {
                    var serializer = new DataContractSerializer(typeof(List<ISpeaker>));
                    speakers = (List<ISpeaker>)serializer.ReadObject(streamReader.BaseStream);
                }
            }
            else
            {
                speakers = new List<ISpeaker>();
            }

            return speakers;
        }
    }
}
