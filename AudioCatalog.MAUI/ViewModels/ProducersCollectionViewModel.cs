using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class ProducersCollectionViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;
        public ICommand OpenAddProducerPageCommand { get; }

        [ObservableProperty]
        private ObservableCollection<ProducerViewModel> producers;

        public ProducersCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;

            producers = new ObservableCollection<ProducerViewModel>();

            LoadData();

            OpenAddProducerPageCommand = new Command(OnOpenAddProducerPage);
        }

        public void LoadData()
        {
            producers.Clear();

            foreach (var producer in _blc.GetAllProducers())
            {
                producers.Add(new ProducerViewModel(producer, _blc));
            }
        }

        private void OnOpenAddProducerPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new AddProducerPage(new AddProducerViewModel(_blc)));
        }        
    }
}
