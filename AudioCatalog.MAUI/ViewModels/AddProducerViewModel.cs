using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class AddProducerViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;

        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string countryOfOrigin;
        [ObservableProperty]
        private string website;

        public AddProducerViewModel(BLC.BLC blc)
        {
            _blc = blc;

            Name = string.Empty;
            CountryOfOrigin = string.Empty;
            Website = string.Empty;

            PropertyChanged += OnPropertyChanged;
        }

        [RelayCommand(CanExecute = nameof(CanSaveProducer))]
        private void SaveProducer()
        {
            
            _blc.CreateProducer(Name, CountryOfOrigin, Website);

            Name = string.Empty;
            CountryOfOrigin = string.Empty;
            Website = string.Empty;

            WeakReferenceMessenger.Default.Send("ProducerAdded");
            Application.Current.MainPage.Navigation.PopAsync();
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
