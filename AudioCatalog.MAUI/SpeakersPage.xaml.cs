using Sudzinski.AudioCatalog.MAUI.ViewModels;

namespace Sudzinski.AudioCatalog.MAUI;

public partial class SpeakersPage : ContentPage
{
	public SpeakersPage(SpeakersCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}