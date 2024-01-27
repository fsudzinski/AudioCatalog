using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Sudzinski.AudioCatalog.MAUI.ViewModels
{
    public partial class SpeakersCollectionViewModel : ObservableObject
    {
        private readonly BLC.BLC _blc;

        [ObservableProperty]
        private ObservableCollection<SpeakerViewModel> speakers;

        public SpeakersCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;

            speakers = new ObservableCollection<SpeakerViewModel>();

            foreach (var speaker in _blc.GetAllSpeakers())
            {
                speakers.Add(new SpeakerViewModel(speaker));
            }
        }
    }
}
