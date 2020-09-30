using Core;
using Core.Models;
using Prism.Events;
using Prism.Mvvm;

namespace Main.ViewModels
{
    public class ShellViewModel : BindableBase
    {

        #region property Title

        private string title = "Drive Info";

        /// <summary>
        /// Represent Title property
        /// </summary>
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }


        IEventAggregator _ea;

        public ShellViewModel(IEventAggregator ea)
        {
            _ea = ea;

            _ea.GetEvent<SelectedDriveEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(DriveInfoModel driveModel)
        {
            if (driveModel?.Name?.Length > 0)
                Title = "Drive info (" + driveModel?.Name + ")";
        }

        /// <summary>
        /// Backing field for property Title
        /// </summary>
        #endregion
    }
}
