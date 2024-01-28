using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Sudzinski.AudioCatalog.Core;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class SpeakersCollectionViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;

        [ObservableProperty]
        private string searchText;
        [ObservableProperty]
        private ProducerViewModel selectedProducer;
        [ObservableProperty]
        private float minPower;
        [ObservableProperty]
        private float maxWeight;
        [ObservableProperty]
        private string selectedColor;

        public List<ProducerViewModel> Producers { set; get; }
        public List<string> Colors { get; } = new List<string> { "All" };


        [ObservableProperty]
        private ObservableCollection<SpeakerViewModel> speakers;

        public ICommand OpenAddSpeakerPageCommand { get; }

        public SpeakersCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;

            speakers = new ObservableCollection<SpeakerViewModel>();

            LoadData();

            foreach (var color in Enum.GetValues(typeof(ColorType)))
            {
                Colors.Add(color.ToString());
            }

            OpenAddSpeakerPageCommand = new Command(OnOpenAddSpeakerPage);
        }

        public void LoadData()
        {
            Speakers.Clear();

            foreach (var speaker in _blc.GetAllSpeakers())
            {
                Speakers.Add(new SpeakerViewModel(speaker, _blc));
            }

            Producers = new List<ProducerViewModel>();
            
            foreach (var producer in _blc.GetAllProducers())
            {
                Producers.Add(new ProducerViewModel(producer, _blc));
            }

            var allProducer = new ProducerViewModel(Producers[0], _blc);
            allProducer.Id = 0;
            allProducer.Name = "All";
            allProducer.CountryOfOrigin = "";
            allProducer.Website = "";
            Producers.Insert(0, allProducer);
        }

        private void OnOpenAddSpeakerPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new AddSpeakerPage(new AddSpeakerViewModel(_blc)));
        }
        public void FilterSpeakers()
        {
            var filteredSpeakers = _blc.GetAllSpeakers();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredSpeakers = filteredSpeakers.Where(s => s.Name.ToLowerInvariant().Contains(SearchText.ToLowerInvariant()));
            }

            if (SelectedProducer != null && SelectedProducer.Id != 0)
            {
                filteredSpeakers = filteredSpeakers.Where(s => s.Producer.Id == SelectedProducer.Id);
            }

            if (MinPower != 0)
            {
                filteredSpeakers = filteredSpeakers.Where(s => s.Power >= MinPower);
            }

            if (MaxWeight != 0)
            {
                filteredSpeakers = filteredSpeakers.Where(s => s.Weight <= MaxWeight);
            }

            if (SelectedColor != "All")
            {
                filteredSpeakers = filteredSpeakers.Where(s => s.Color.ToString() == SelectedColor);
            }

            Speakers.Clear();
            foreach (var speaker in filteredSpeakers)
            {
                Speakers.Add(new SpeakerViewModel(speaker, _blc));
            }
        }
    }
}
