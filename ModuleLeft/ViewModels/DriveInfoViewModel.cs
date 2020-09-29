using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.BL;
using Prism.Mvvm;
using Prism.Events;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Management;

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

        private DriveInfoModel _driveInfo = new DriveInfoModel { Name = "Test", Caption = "cap", Type = DiskType.Fixed };
        public DriveInfoModel SelectedDriveInfo
        {
            get { return _driveInfo; }
            set { SetProperty(ref _driveInfo, value); }
        }

        public DelegateCommand SendSelectedDriveInfoCommand { get; private set; }

        public DriveInfoViewModel(IEventAggregator ea)
        {
            DriveInfoCollection = new ObservableCollection<DriveInfoModel>();
            foreach (var drive in drives.GetDrives())
            {
                Collection.Add(drive);
            }

            watcher = new ManagementEventWatcher();
            Task.Run(() => WatchChanges(watcher));




            _ea = ea;
            SendSelectedDriveInfoCommand = new DelegateCommand(PublishSelectedDriveInfo);
        }

        private void PublishSelectedDriveInfo()
        {
            _ea.GetEvent<MessageSentEvent>().Publish(SelectedDriveInfo);
        }




        public ObservableCollection<DriveInfoModel> Collection { get; set; }
        private readonly ManagementEventWatcher watcher;
        private DriveInfoViewModel selectedDrive;
        private readonly CollectDrives drives = new CollectDrives();

        private ManagementEventWatcher WatchChanges(ManagementEventWatcher watcher)
        {
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 OR EventType = 3");
            watcher.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
            watcher.Query = query;
            watcher.Start();
            watcher.WaitForNextEvent();
            return watcher;
        }

        private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            IList<DriveInfoModel> currentList = drives.GetDrives();
            IList<DriveInfoModel> previousList = Collection.ToList();

            if (currentList.Count > previousList.Count)
            {
                foreach (var drive in currentList)
                {
                    if (!Collection.Any(p => p.ToString().Equals(drive.ToString())))
                    {
                        Collection.Add(drive);
                    }
                }
            }
            else
            {
                foreach (var drive in previousList)
                {
                    if (!currentList.Any(p => p.ToString().Equals(drive.ToString())))
                    {
                        Collection.Remove(drive);
                    }
                }
            }
        }
    }
}
