using Microsoft.EntityFrameworkCore;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.DAOSQL.BO;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.DAOSQL
{
    public class DAO : DbContext, IDAO
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DAOSQL.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producer>()
                .HasMany(b => (ICollection<Speaker>)b.Speakers)
                .WithOne(a => (Producer)a.Producer);            
        }

        public DbSet<Producer> Producers { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

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

        public void CreateProducer(string name, string countryOfOrigin, string website)
        {
            Producer producer = new Producer()
            {
                Id = GenerateUniqueId(Producers, producer => producer.Id),
                CountryOfOrigin = countryOfOrigin,
                Name = name,
                Website = website
            };
            Producers.Add(producer);
            SaveChanges();
        }

        public void CreateSpeaker(string name, int producerId, float power, float weight, ColorType color)
        {
            Speaker speaker = new Speaker()
            {
                Id = GenerateUniqueId(Speakers, speaker => speaker.Id),
                Name = name,
                Producer = Producers.Find(producerId),
                Power = power,
                Weight = weight,
                Color = color
            };
            Speakers.Add(speaker);
            SaveChanges();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return Producers;
        }

        public IEnumerable<ISpeaker> GetAllSpeakers()
        {
            return Speakers.Include(b => b.Producer);
        }

        public void UpdateProducer(int id, string name, string countryOfOrigin, string website)
        {
            IProducer? producer = Producers.Find(id);
            if (producer != null)
            {
                producer.Name = name;
                producer.CountryOfOrigin = countryOfOrigin;
                producer.Website = website;
                SaveChanges();
            }
        }

        public void UpdateSpeaker(int id, string name, int producerId, float power, float weight, ColorType color)
        {
            ISpeaker? speaker = Speakers.Find(id);
            if (speaker != null)
            {
                speaker.Name = name;
                speaker.Producer = Producers.Find(producerId);
                speaker.Power = power;
                speaker.Weight = weight;
                speaker.Color = color;
                SaveChanges();
            }
        }

        public void DeleteProducer(int id)
        {
            Producer? producer = Producers.Find(id);
            if (producer != null)
            {
                Producers.Remove(producer);
                SaveChanges();
            }
        }

        public void DeleteSpeaker(int id)
        {
            Speaker? speaker = Speakers.Find(id);
            if (speaker != null)
            {
                Speakers.Remove(speaker);
                SaveChanges();
            }
        }
        public IProducer? GetProducerById(int id)
        {
            return Producers.Include(p => p.Speakers).FirstOrDefault(p => p.Id == id);
        }

        public ISpeaker? GetSpeakerById(int id)
        {
            return Speakers.Include(s => s.Producer).FirstOrDefault(s => s.Id == id);
        }
    }
}
