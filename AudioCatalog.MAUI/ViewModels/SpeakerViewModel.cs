using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class SpeakerViewModel : ObservableObject, ISpeaker
    {
        private readonly BLC.BLC _blc;
        public ICommand OpenEditSpeakerPageCommand { get; }
        public ICommand DeleteSpeakerCommand { get; }

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

        public SpeakerViewModel(ISpeaker speaker, BLC.BLC blc)
        {
            Id = speaker.Id;
            Name = speaker.Name;
            Producer = speaker.Producer;
            Power = speaker.Power;
            Weight = speaker.Weight;
            Color = speaker.Color;

            _blc = blc;
            OpenEditSpeakerPageCommand = new Command<SpeakerViewModel>(OnOpenEditSpeakerPage);
            DeleteSpeakerCommand = new Command(async () => await OnDeleteSpeaker());
        }
        private void OnOpenEditSpeakerPage(SpeakerViewModel speaker)
        {
            App.Current.MainPage.Navigation.PushAsync(new EditSpeakerPage(new EditSpeakerViewModel(speaker, _blc)));
        }

        private async Task OnDeleteSpeaker()
        {
            bool result = await App.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this speaeker?", "Yes", "No");
            if (result)
            {
                _blc.DeleteSpeaker(Id);
                WeakReferenceMessenger.Default.Send("SpeakerDeleted");
            }
        }
    }
}
