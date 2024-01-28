using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class AddSpeakerViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;

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

        public AddSpeakerViewModel(BLC.BLC blc)
        {
            _blc = blc;

            Name = string.Empty;
            Producer = null;
            Power = 0;
            Weight = 0;
            Color = ColorType.Black;

            PropertyChanged += OnPropertyChanged;
        }

        [RelayCommand(CanExecute = nameof(CanSaveSpeaker))]
        private void SaveSpeaker()
        {
            _blc.CreateSpeaker(Name, Producer.Id, Power, Weight, Color);

            WeakReferenceMessenger.Default.Send("SpeakerAdded");
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
