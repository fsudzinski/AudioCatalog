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
        ProducersPicker.SelectedIndex = 0;
        ColorPicker.SelectedIndex = 0;

        WeakReferenceMessenger.Default.Register<string>(this, (r, m) =>
        {
            if (m.Equals("SpeakerAdded") || m.Equals("SpeakerDeleted") || m.Equals("SpeakerUpdated") || m.Equals("ProducerDeleted"))
            {
                viewModel.LoadData();
            }
        });
    }
    private void ProducerPicker_SelectedItemChanged(object sender, EventArgs e)
    {
        ViewModel.FilterSpeakers();
    }

    private void ProducersSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel.FilterSpeakers();
    }

    private void MinPower_TextChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel.FilterSpeakers();
    }

    private void MaxWeight_TextChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel.FilterSpeakers();
    }

    private void ColorPicker_SelectedItemChanged(object sender, EventArgs e)
    {
        ViewModel.FilterSpeakers();
    }
}