using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string libraryName = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"];
            BLC.BLC blc = new BLC.BLC(libraryName);

            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["test"]);

            foreach (IProducer producer in blc.GetAllProducers())
            {
                Console.WriteLine(producer.Name);
            }

            foreach (ISpeaker speaker in blc.GetAllSpeakers())
            {
                Console.WriteLine(speaker.Name);
            }
        }
    }
}
