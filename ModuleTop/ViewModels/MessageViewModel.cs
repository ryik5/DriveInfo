using Prism.Events;
using Prism.Mvvm;
using Core;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace ModuleTop.ViewModels
{
    class MessageViewModel : BindableBase
    {
        IEventAggregator _ea;

        private DriveInfoModel _message = new DriveInfoModel { Name = "Test" };
        public DriveInfoModel Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand SendMessageCommand { get; private set; }

        public MessageViewModel(IEventAggregator ea)
        {
            _ea = ea;
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        private void SendMessage()
        {
            _ea.GetEvent<SelectedDriveEvent>().Publish(Message);
        }
    }
}
