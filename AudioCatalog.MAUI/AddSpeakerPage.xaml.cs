using Sudzinski.AudioCatalog.MAUI.ViewModels;

namespace Sudzinski.AudioCatalog.MAUI;

public partial class AddSpeakerPage : ContentPage
{
	public AddSpeakerPage(AddSpeakerViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}