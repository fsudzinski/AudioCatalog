using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class AddProducerViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;
        public ICommand SaveProducerCommand { get; }

        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string countryOfOrigin;
        [ObservableProperty]
        private string website;

        public AddProducerViewModel(BLC.BLC blc)
        {
            _blc = blc;
            SaveProducerCommand = new Command(OnSaveProducer);

            Name = string.Empty;
            CountryOfOrigin = string.Empty;
            Website = string.Empty;
        }

        private void OnSaveProducer()
        {
            if (!string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(CountryOfOrigin)
                && !string.IsNullOrWhiteSpace(Website))
            {
                _blc.CreateProducer(Name, CountryOfOrigin, Website);

                Name = string.Empty;
                CountryOfOrigin = string.Empty;
                Website = string.Empty;

                MessagingCenter.Send(this, "ProducerAdded");

                Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
