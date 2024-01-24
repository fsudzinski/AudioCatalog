namespace Sudzinski.AudioCatalog.Interfaces
{
    public interface IProducer
    {
        int Id { get; set; }
        string Name { get; set; }
        string CountryOfOrigin { get; set; }
        string Website { get; set; }
        List<ISpeaker> Speakers { get; set; }
    }
}
