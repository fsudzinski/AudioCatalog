using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class EditProducerViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;

        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string countryOfOrigin;
        [ObservableProperty]
        private string website;

        public EditProducerViewModel(ProducerViewModel producer, BLC.BLC blc)
        {
            _blc = blc;

            Id = producer.Id;
            Name = producer.Name;
            CountryOfOrigin = producer.CountryOfOrigin;
            Website = producer.Website;

            PropertyChanged += OnPropertyChanged;
        }

        [RelayCommand(CanExecute = nameof(CanSaveProducer))]
        private void SaveProducer()
        {
            if (!string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(CountryOfOrigin)
                && !string.IsNullOrWhiteSpace(Website))
            {
                _blc.UpdateProducer(Id, Name, CountryOfOrigin, Website);
                WeakReferenceMessenger.Default.Send("ProducerUpdated");
                Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private bool CanSaveProducer()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(CountryOfOrigin)
                && !string.IsNullOrWhiteSpace(Website);
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveProducerCommand.NotifyCanExecuteChanged();
        }
    }
}
