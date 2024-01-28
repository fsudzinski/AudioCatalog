using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class ProducersCollectionViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;

        [ObservableProperty]
        private string searchText;
        [ObservableProperty]
        private string selectedCountry;
        [ObservableProperty]
        private ObservableCollection<ProducerViewModel> producers;

        [ObservableProperty]
        public ObservableCollection<string> countries;    
        private string countriesPickerPlaceholder = "All";

        public ICommand OpenAddProducerPageCommand { get; }

        public ProducersCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;

            Producers = new ObservableCollection<ProducerViewModel>();
            Countries = new ObservableCollection<string>();

            LoadData();

            OpenAddProducerPageCommand = new Command(OnOpenAddProducerPage);
        }

        public void LoadData()
        {
            Producers.Clear();

            foreach (var producer in _blc.GetAllProducers())
            {
                Producers.Add(new ProducerViewModel(producer, _blc));
            }


            Countries.Clear();
            Countries.Add(countriesPickerPlaceholder);
            foreach (var country in Producers.Select(p => p.CountryOfOrigin).Distinct().ToList())
            {
                Countries.Add(country);
            }
        }

        private void OnOpenAddProducerPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new AddProducerPage(new AddProducerViewModel(_blc)));
        }

        public void FilterProducers()
        {
            var filteredProducers = _blc.GetAllProducers();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredProducers = filteredProducers.Where(p => p.Name.ToLowerInvariant().Contains(SearchText.ToLowerInvariant()));
            }

            if (!string.IsNullOrWhiteSpace(SelectedCountry) && !string.Equals(SelectedCountry, countriesPickerPlaceholder))
            {
                filteredProducers = filteredProducers.Where(p => p.CountryOfOrigin.ToLowerInvariant() == SelectedCountry.ToLowerInvariant());
            }

            var newProducersCollection = new ObservableCollection<ProducerViewModel>();

            foreach (var producer in filteredProducers)
            {
                newProducersCollection.Add(new ProducerViewModel(producer, _blc));
            }

            Producers = newProducersCollection;
        }

    }
}
