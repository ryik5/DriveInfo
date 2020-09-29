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
        private ObservableCollection<DriveInfoModel> _driveInfoCollection;
        public ObservableCollection<DriveInfoModel> DriveInfoCollection
        {
            get { return _driveInfoCollection; }
            set { SetProperty(ref _driveInfoCollection, value); }
        }


        IEventAggregator _ea;

        private DriveInfoModel _driveInfo = new DriveInfoModel { Name = "Test", Caption = "cap", Type = DiskType.Fixed };
        public DriveInfoModel SelectedDriveInfo
        {
            get { return _driveInfo; }
            set { 
                SetProperty(ref _driveInfo, value);

                _ea.GetEvent<MessageSentEvent>().Publish(_driveInfo);

            }
        }

        //public DelegateCommand SendSelectedDriveInfoCommand { get; private set; }

        public DriveInfoViewModel(IEventAggregator ea)
        {
            DriveInfoCollection = new ObservableCollection<DriveInfoModel>();
            foreach (var drive in drives.GetDrives())
            {
                DriveInfoCollection.Add(drive);
            }

            watcher = new ManagementEventWatcher();
            Task.Run(() => WatchChanges(watcher));
            

            _ea = ea;
            //SendSelectedDriveInfoCommand = new DelegateCommand(PublishSelectedDriveInfo);
        }

        //private void PublishSelectedDriveInfo()
        //{
        //    _ea.GetEvent<MessageSentEvent>().Publish(SelectedDriveInfo);
        //}




        private readonly ManagementEventWatcher watcher;
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
            IList<DriveInfoModel> previousList = DriveInfoCollection.ToList();

            if (currentList.Count > previousList.Count)
            {
                foreach (var drive in currentList)
                {
                    if (!DriveInfoCollection.Any(p => p.ToString().Equals(drive.ToString())))
                    {
                        DriveInfoCollection.Add(drive);
                    }
                }
            }
            else
            {
                foreach (var drive in previousList)
                {
                    if (!currentList.Any(p => p.ToString().Equals(drive.ToString())))
                    {
                        DriveInfoCollection.Remove(drive);
                    }
                }
            }
        }
    }
}
