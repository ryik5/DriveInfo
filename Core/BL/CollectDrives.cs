using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Threading.Tasks;
using Core.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;

namespace Core.BL
{
    public class CollectDrives
    {
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
            return new DriveInfoModel
            {
                Name = d.Name,
                Caption = d.Name + d.TotalFreeSpace + d.TotalSize + d.DriveType.ToString(),
                FileSystem = GetFileSystem(d.DriveFormat),
                Type = GetDriveType(d.DriveType.ToString()),
                TotalSpace = d.TotalSize,
                FreeSpace = d.TotalFreeSpace
            };
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
