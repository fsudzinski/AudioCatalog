using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Sudzinski.AudioCatalog.Core;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class SpeakersCollectionViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;

        [ObservableProperty]
        private string searchText;
        [ObservableProperty]
        private string selectedProducer;
        [ObservableProperty]
        private string powerText;
        [ObservableProperty]
        private string weightText;
        [ObservableProperty]
        private string selectedColor;

        public List<string> Producers { set; get; }
        private string producersPickerPlaceholder = "All";

        public ColorType[] Colors = (ColorType[])Enum.GetValues(typeof(ColorType));

        [ObservableProperty]
        private ObservableCollection<SpeakerViewModel> speakers;

        public ICommand OpenAddSpeakerPageCommand { get; }

        public SpeakersCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;

            speakers = new ObservableCollection<SpeakerViewModel>();

            LoadData();

            OpenAddSpeakerPageCommand = new Command(OnOpenAddSpeakerPage);
        }

        public void LoadData()
        {
            Speakers.Clear();

            foreach (var speaker in _blc.GetAllSpeakers())
            {
                Speakers.Add(new SpeakerViewModel(speaker, _blc));
            }

            Producers = _blc.GetAllProducers().Select(p => p.Name).Distinct().ToList();
            Producers.Insert(0, producersPickerPlaceholder);
        }

        private void OnOpenAddSpeakerPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new AddSpeakerPage(new AddSpeakerViewModel(_blc)));
        }
        //public void FilterProducers()
        //{
        //    var filteredProducers = _blc.GetAllProducers();

        //    if (!string.IsNullOrWhiteSpace(SearchText))
        //    {
        //        filteredProducers = filteredProducers.Where(p => p.Name.ToLowerInvariant().Contains(SearchText.ToLowerInvariant()));
        //    }

        //    if (!string.IsNullOrWhiteSpace(SelectedCountry) && !string.Equals(SelectedCountry, countriesPickerPlaceholder))
        //    {
        //        filteredProducers = filteredProducers.Where(p => p.CountryOfOrigin.ToLowerInvariant() == SelectedCountry.ToLowerInvariant());
        //    }

        //    Producers.Clear();
        //    foreach (var producer in filteredProducers)
        //    {
        //        Producers.Add(new ProducerViewModel(producer, _blc));
        //    }
        //}
    }
}
