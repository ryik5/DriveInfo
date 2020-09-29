using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Prism.Mvvm;
using Prism.Events;
using Prism.Commands;
using System.Collections.ObjectModel;

namespace ModuleLeft.ViewModels
{
    public class DriveInfoViewModel : BindableBase
    {


        //IEventAggregator _eaList;

        private ObservableCollection<DriveInfoModel> _driveInfoCollection;
        public ObservableCollection<DriveInfoModel> DriveInfoCollection
        {
            get { return _driveInfoCollection; }
            set { SetProperty(ref _driveInfoCollection, value); }
        }

        //public void MessageListViewModel(IEventAggregator ea)
        //{
        //    _eaList = ea;
        //    DriveInfoCollection = new ObservableCollection<DriveInfoModel>();

        //    _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived1);
        //}

        //private void MessageReceived1(DriveInfoModel message)
        //{
        //    DriveInfoCollection.Add(message);
        //}







        IEventAggregator _ea;

        private DriveInfoModel _driveInfo = new DriveInfoModel {Name="Test", Caption="cap", Type= DiskType.Fixed };
        public DriveInfoModel SelectedDriveInfo
        {
            get { return _driveInfo; }
            set { SetProperty(ref _driveInfo, value); }
        }

        public DelegateCommand SendSelectedDriveInfoCommand { get; private set; }

        public DriveInfoViewModel(IEventAggregator ea)
        {
            _ea = ea;
            SendSelectedDriveInfoCommand = new DelegateCommand(PublishSelectedDriveInfo);
        }

        private void PublishSelectedDriveInfo()
        {
            _ea.GetEvent<MessageSentEvent>().Publish(SelectedDriveInfo);
        }
    }
}
