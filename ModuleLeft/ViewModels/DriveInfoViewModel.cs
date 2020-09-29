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

        public DriveInfoViewModel(IEventAggregator ea)
        {
            //DriveInfoCollection = new ObservableCollection<DriveInfoModel>();
            drives = new CollectDrives();
            DriveInfoCollection = drives.driveList;
            //foreach (var drive in drives.GetDrives())
            //{
            //    DriveInfoCollection.Add(drive);
            //}
            
            _ea = ea;
        }
        

        private  CollectDrives drives = new CollectDrives();

   }
}
