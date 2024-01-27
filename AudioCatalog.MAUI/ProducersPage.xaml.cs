using Sudzinski.AudioCatalog.MAUI.ViewModels;

namespace Sudzinski.AudioCatalog.MAUI;

public partial class ProducersPage : ContentPage
{
    public ProducersPage(ProducersCollectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        MessagingCenter.Subscribe<AddProducerViewModel>(this, "ProducerAdded", (sender) =>
        {
            viewModel.LoadData();
        });

        MessagingCenter.Subscribe<EditProducerViewModel>(this, "ProducerUpdated", (sender) =>
        {
            viewModel.LoadData();
        });

        MessagingCenter.Subscribe<ProducerViewModel>(this, "ProducerDeleted", (sender) =>
        {
            viewModel.LoadData();
        });
    }
}
