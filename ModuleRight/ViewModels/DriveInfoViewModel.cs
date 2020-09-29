using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Models;

namespace ModuleRight.ViewModels
{
    public class DriveInfoViewModel : BindableBase
    {
        IEventAggregator _ea;

        private DriveInfoModel _driveModel;
        public DriveInfoModel DriveModel
        {
            get { return _driveModel; }
            set { SetProperty(ref _driveModel, value); }
        }

        public DriveInfoViewModel(IEventAggregator ea)
        {
            DriveModel = new DriveInfoModel();

            _ea = ea;

            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(DriveInfoModel driveModel)
        {
            DriveModel = driveModel;
        }
    }
}
