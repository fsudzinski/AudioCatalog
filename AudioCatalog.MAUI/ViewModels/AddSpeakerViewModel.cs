using System.Collections.ObjectModel;
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
        private float power;
        [ObservableProperty]
        private float weight;

        [ObservableProperty]
        private ObservableCollection<ProducerViewModel> producers;
        [ObservableProperty]
        private ProducerViewModel selectedProducer;

        [ObservableProperty]
        public ColorType[] colors = (ColorType[])Enum.GetValues(typeof(ColorType));
        [ObservableProperty]
        private ColorType selectedColor;

        public AddSpeakerViewModel(BLC.BLC blc)
        {
            _blc = blc;

            Name = string.Empty;
            Power = 0;
            Weight = 0;

            Producers = new ObservableCollection<ProducerViewModel>();

            Producers.Clear();
            foreach (var producer in _blc.GetAllProducers())
            {
                Producers.Add(new ProducerViewModel(producer, _blc));
            }

            PropertyChanged += OnPropertyChanged;
        }

        [RelayCommand(CanExecute = nameof(CanSaveSpeaker))]
        private void SaveSpeaker()
        {
            _blc.CreateSpeaker(Name, SelectedProducer.Id, Power, Weight, selectedColor);

            WeakReferenceMessenger.Default.Send("SpeakerAdded");
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private bool CanSaveSpeaker()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && SelectedProducer != null
                && Power > 0
                && Weight > 0;
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveSpeakerCommand.NotifyCanExecuteChanged();
        }
    }
}
