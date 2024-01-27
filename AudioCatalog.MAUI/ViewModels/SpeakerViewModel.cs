using CommunityToolkit.Mvvm.ComponentModel;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class SpeakerViewModel : ObservableObject, ISpeaker
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private IProducer producer;
        [ObservableProperty]
        private float power;
        [ObservableProperty]
        private float weight;
        [ObservableProperty]
        private ColorType color;

        public SpeakerViewModel(ISpeaker speaker)
        {
            Id = speaker.Id;
            Name = speaker.Name;
            Producer = speaker.Producer;
            Power = speaker.Power;
            Weight = speaker.Weight;
            Color = speaker.Color;
        }
    }
}
