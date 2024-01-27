using Sudzinski.AudioCatalog.MAUI.ViewModels;

namespace Sudzinski.AudioCatalog.MAUI;

public partial class EditProducerPage : ContentPage
{
	public EditProducerPage(EditProducerViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}