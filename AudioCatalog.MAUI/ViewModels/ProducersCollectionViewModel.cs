using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

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

        public List<string> Countries { set; get; }
        private string countriesPickerPlaceholder = "All";


        public ICommand OpenAddProducerPageCommand { get; }
        public ICommand SearchCommand { get; }
        //public ICommand FilterByCountryCommand { get; }

        public ProducersCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;

            producers = new ObservableCollection<ProducerViewModel>();

            LoadData();

            OpenAddProducerPageCommand = new Command(OnOpenAddProducerPage);
            SearchCommand = new Command<string>(PerformSearch);
            //FilterByCountryCommand = new Command(FilterByCountry);
        }

        public void LoadData()
        {
            Producers.Clear();

            foreach (var producer in _blc.GetAllProducers())
            {
                Producers.Add(new ProducerViewModel(producer, _blc));
            }

            Countries = Producers.Select(p => p.CountryOfOrigin).Distinct().ToList();
            Countries.Insert(0, countriesPickerPlaceholder);
        }

        private void OnOpenAddProducerPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new AddProducerPage(new AddProducerViewModel(_blc)));
        }
        private void PerformSearch(string searchText)
        {
            LoadData();
            if (!string.IsNullOrWhiteSpace(searchText))
            {            
                searchText = searchText.ToLowerInvariant();

                var filteredProducers = new ObservableCollection<ProducerViewModel>(
                    Producers.Where(p => p.Name.ToLowerInvariant().Contains(searchText))
                );

                Producers = filteredProducers;
            }
        }
        private void FilterByCountry(object sender, EventArgs e)
        {
            LoadData();
            if (!string.Equals(SelectedCountry, countriesPickerPlaceholder))
            {
                SelectedCountry = SelectedCountry.ToLowerInvariant();

                var filteredProducers = new ObservableCollection<ProducerViewModel>(
                    Producers.Where(p => p.CountryOfOrigin.ToLowerInvariant() == SelectedCountry)
                );
                
                Producers = filteredProducers;
            }
        }
    }
}
