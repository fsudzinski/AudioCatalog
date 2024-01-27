namespace Sudzinski.AudioCatalog.MAUI;
using Sudzinski.AudioCatalog.MAUI.ViewModels;

public partial class AddProducerPage : ContentPage
{
	public AddProducerPage(AddProducerViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}