using Sudzinski.AudioCatalog.MAUI.ViewModels;
namespace Sudzinski.AudioCatalog.MAUI;

public partial class EditSpeakerPage : ContentPage
{
	public EditSpeakerPage(EditSpeakerViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}