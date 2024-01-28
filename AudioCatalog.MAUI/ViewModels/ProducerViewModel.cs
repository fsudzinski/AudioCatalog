using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class ProducerViewModel : ObservableObject, IProducer
    {
        private readonly BLC.BLC _blc;
        public ICommand OpenEditProducerPageCommand { get; }
        public ICommand DeleteProducerCommand { get; }

        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string countryOfOrigin;
        [ObservableProperty]
        private string website;
        [ObservableProperty]
        private List<ISpeaker> speakers;

        public ProducerViewModel(IProducer producer, BLC.BLC blc)
        {
            Id = producer.Id;
            Name = producer.Name;
            CountryOfOrigin = producer.CountryOfOrigin;
            Website = producer.Website;
            Speakers = producer.Speakers;

            _blc = blc;
            OpenEditProducerPageCommand = new Command<ProducerViewModel>(OnOpenEditProducerPage);
            DeleteProducerCommand = new Command(async () => await OnDeleteProducer());

        }

        private void OnOpenEditProducerPage(ProducerViewModel producer)
        {
            App.Current.MainPage.Navigation.PushAsync(new EditProducerPage(new EditProducerViewModel(producer, _blc)));
        }

        private async Task OnDeleteProducer()
        {
            bool result = await App.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this producer?\n\nWARNING: All speakers associated to this producer will be deleted!", "Yes", "No");
            if (result)
            {
                _blc.DeleteProducer(Id);
                WeakReferenceMessenger.Default.Send("ProducerDeleted");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
