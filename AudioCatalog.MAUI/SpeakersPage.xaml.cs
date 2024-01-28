using CommunityToolkit.Mvvm.Messaging;
using Sudzinski.AudioCatalog.MAUI.ViewModels;

namespace Sudzinski.AudioCatalog.MAUI;

public partial class SpeakersPage : ContentPage
{
    private SpeakersCollectionViewModel ViewModel => (SpeakersCollectionViewModel)BindingContext;
    public SpeakersPage(SpeakersCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        WeakReferenceMessenger.Default.Register<string>(this, (r, m) =>
        {
            if (m.Equals("SpeakerAdded") || m.Equals("SpeakerDeleted") || m.Equals("SpeakerUpdated") || m.Equals("ProducerDeleted"))
            {
                viewModel.LoadData();
            }
        });
    }
    //private void CountryPicker_SelectedItemChanged(object sender, EventArgs e)
    //{
    //    ViewModel.FilterProducers();
    //}

    //private void ProducersSearch_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    ViewModel.FilterProducers();
    //}
}