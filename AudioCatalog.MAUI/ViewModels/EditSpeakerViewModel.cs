using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;


namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class EditSpeakerViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;

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

        public EditSpeakerViewModel(SpeakerViewModel speaker, BLC.BLC blc)
        {
            _blc = blc;

            Id = speaker.Id;
            Name = speaker.Name;
            Producer = speaker.Producer;
            Power = speaker.Power;
            Weight = speaker.Weight;
            Color = speaker.Color;

            PropertyChanged += OnPropertyChanged;
        }

        [RelayCommand(CanExecute = nameof(CanSaveSpeaker))]
        private void SaveSpeaker()
        {            
            _blc.UpdateSpeaker(Id, Name, Producer.Id, Power, Weight, Color);
            WeakReferenceMessenger.Default.Send("SpeakerUpdated");
            Application.Current.MainPage.Navigation.PopAsync();            
        }

        private bool CanSaveSpeaker()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && Power > 0
                && Weight > 0;
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveSpeakerCommand.NotifyCanExecuteChanged();
        }
    }
}
