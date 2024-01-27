using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class EditProducerViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;
        public ICommand SaveProducerCommand { get; }

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
            SaveProducerCommand = new Command(OnSaveProducer);

            Id = producer.Id;
            Name = producer.Name;
            CountryOfOrigin = producer.CountryOfOrigin;
            Website = producer.Website;
        }

        private void OnSaveProducer()
        {
            if (!string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(CountryOfOrigin)
                && !string.IsNullOrWhiteSpace(Website))
            {
                _blc.UpdateProducer(Id, Name, CountryOfOrigin, Website);

                MessagingCenter.Send(this, "ProducerUpdated");

                Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
