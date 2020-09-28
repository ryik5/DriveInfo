using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DriveInfoModel : INotifyPropertyChanged, IEquatable<DriveInfoModel>
    {
        private string name;
        private string caption;
        private DiskType type;
        private FileSystem fileSystem;
        private double totalSpace;
        private double freeSpace;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                OnPropertyChanged("Caption");
            }
        }
        public DiskType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public FileSystem FileSystem
        {
            get { return fileSystem; }
            set
            {
                fileSystem = value;
                OnPropertyChanged("FileSystem");
            }
        }
        public double TotalSpace
        {
            get { return totalSpace; }
            set
            {
                totalSpace = value;
                OnPropertyChanged("TotalSpace");
            }
        }
        public double FreeSpace
        {
            get { return freeSpace; }
            set
            {
                freeSpace = value;
                OnPropertyChanged("FreeSpace");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public override string ToString()
        {
            return name + type + fileSystem + totalSpace;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            DriveInfoModel objAsPart = obj as DriveInfoModel;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(DriveInfoModel other)
        {
            if (other == null) return false;
            return (this.ToString().Equals(other.ToString()));
        }
    }
}
