using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Threading.Tasks;
using Core.Models;
using System.Linq;
using System.Collections.ObjectModel;

namespace Core.BL
{
    public class CollectDrives
    {
        public ObservableCollection<DriveInfoModel> driveList { get; private set; }
        public IList<DriveInfoModel> GetDrives()
        {
            IList<DriveInfoModel> driveList = new List<DriveInfoModel>();
            DriveInfoModel drive;

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady)
                {
                    drive = GetDriveInfo(d);

                    driveList.Add(drive);
                }
            }

            return driveList;
        }

        private DriveInfoModel GetDriveInfo(DriveInfo d)
        {
            DriveInfoModel drive = new DriveInfoModel();

            string name = d.Name;
            double totalSpace = d.TotalSize;
            double freeSpace = d.TotalFreeSpace;
            string fileSystem = d.DriveFormat;
            string mediaType = d.DriveType.ToString();

            drive.Name = name;
            drive.Caption = name + fileSystem + totalSpace + mediaType;
            drive.FileSystem = GetFileSystem(fileSystem);
            drive.Type = GetDriveType(mediaType);
            drive.TotalSpace = totalSpace;
            drive.FreeSpace = freeSpace;

            return drive;
        }


        public CollectDrives()
        {
            driveList = new ObservableCollection<DriveInfoModel>();
            driveList.AddRange(GetDrives());
            watcher = new ManagementEventWatcher();
          Task.Run(()=>  WatchChanges(watcher));
        }


        private readonly ManagementEventWatcher watcher;
        public ManagementEventWatcher WatchChanges(ManagementEventWatcher watcher)
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
            IList<DriveInfoModel> currentList = GetDrives();
            IList<DriveInfoModel> previousList = driveList.ToList();

            if (currentList.Count > previousList.Count)
            {
                foreach (var drive in currentList)
                {
                    if (!driveList.Any(p => p.ToString().Equals(drive.ToString())))
                    {
                        driveList.Add(drive);
                    }
                }
            }
            else
            {
                foreach (var drive in previousList)
                {
                    if (!currentList.Any(p => p.ToString().Equals(drive.ToString())))
                    {
                        driveList.Remove(drive);
                    }
                }
            }
        }
        

        private FileSystem GetFileSystem(string type)
        {
            if (type?.Length == 0)
            {
                return FileSystem.Unknown;
            }
            else if (type.ToLower().Contains("fat32"))
            {
                return FileSystem.FAT32;
            }
            else if (type.ToLower().Contains("ntfs"))
            {
                return FileSystem.NTFS;
            }
            else
            {
                return FileSystem.Unknown;
            }
        }

        private DiskType GetDriveType(string type)
        {
            if (type?.Length == 0)
            {
                return DiskType.Unknown;
            }
            else if (type.ToLower().Contains("remov"))
            {
                return DiskType.Removable;
            }
            else if (type.ToLower().Contains("fix"))
            {
                return DiskType.Fixed;
            }
            else
            {
                return DiskType.Unknown;
            }
        }

    }
}
