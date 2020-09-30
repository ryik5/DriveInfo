using Core;
using Core.BL;
using Core.Models;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Management;

namespace ModuleBackground.ViewModels
{
    public class ModuleBackgroundViewModel : BindableBase
    {
        IEventAggregator _ea;

        public ModuleBackgroundViewModel(IEventAggregator ea)
        {
            _ea = ea;

            worker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            worker.DoWork +=new DoWorkEventHandler( worker_DoWork);
            worker.RunWorkerAsync(null);
        }

        public void StopCollectDriveInfo()
        {
            watcher.Stop();
            watcher.Dispose();
            worker.CancelAsync();
            worker.Dispose();
        }

        BackgroundWorker worker;
        ManagementEventWatcher watcher;
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 OR EventType = 3");

            watcher = new ManagementEventWatcher();
            watcher.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
            watcher.Stopped += new StoppedEventHandler(Watcher_Stopped);
            watcher.Query = query;
            watcher.Start();

            while (collectDriveInformation)
            {
                watcher.WaitForNextEvent();
            }
        }

        bool collectDriveInformation = true;
        private void Watcher_Stopped(object sender, StoppedEventArgs e)
        {
            collectDriveInformation = false;
        }

        private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            CollectDrives _driveInfoCollection = new CollectDrives();
            ObservableCollection<DriveInfoModel> driveCollection = new ObservableCollection<DriveInfoModel>();

            driveCollection.AddRange(_driveInfoCollection.GetDrives());

            _ea.GetEvent<CollectionChangedEvent>().Publish(driveCollection);
        }
    }
}