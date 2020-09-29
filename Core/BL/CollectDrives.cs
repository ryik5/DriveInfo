using System.Collections.Generic;
using System.IO;
using Core.Models;

namespace Core.BL
{
    public class CollectDrives
    {
        IList<DriveInfoModel> driveList;
        public IList<DriveInfoModel> GetDrives()
        {
            driveList = new List<DriveInfoModel>();
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
