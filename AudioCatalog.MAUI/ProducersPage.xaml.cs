using CommunityToolkit.Mvvm.Messaging;
using Sudzinski.AudioCatalog.MAUI.ViewModels;

namespace Sudzinski.AudioCatalog.MAUI;

public partial class ProducersPage : ContentPage
{
    private ProducersCollectionViewModel ViewModel => (ProducersCollectionViewModel)BindingContext;

    public ProducersPage(ProducersCollectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        CountryPicker.SelectedIndex = 0;

        WeakReferenceMessenger.Default.Register<string>(this, (r, m) =>
        {
            if (m.Equals("ProducerAdded") || m.Equals("ProducerDeleted") || m.Equals("ProducerUpdated"))
            {
                viewModel.LoadData();
            }            
        });
    }

    private void CountryPicker_SelectedItemChanged(object sender, EventArgs e)
    {
        ViewModel.FilterProducers();
    }

    private void ProducersSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel.FilterProducers();
    }
}
