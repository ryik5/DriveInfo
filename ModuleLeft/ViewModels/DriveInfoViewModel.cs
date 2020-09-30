using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.BL;
using Prism.Mvvm;
using Prism.Events;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace ModuleLeft.ViewModels
{
    public class DriveInfoViewModel : BindableBase, INotifyPropertyChanged
    {

        public ObservableCollection<DriveInfoModel> 
            DriveInfoCollection { get; } = new ObservableCollection<DriveInfoModel>();


        IEventAggregator _ea;

        private DriveInfoModel _driveInfo;
        public DriveInfoModel SelectedDriveInfo
        {
            get { return _driveInfo; }
            set
            {
                SetProperty(ref _driveInfo, value);

                _ea.GetEvent<SelectedDriveEvent>().Publish(_driveInfo);
            }
        }


        private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        public DelegateCommand ChangeCollection { get; set; }

        private void CollectionReceived(ObservableCollection<DriveInfoModel> driveCollection)
        {
            ChangeCollection = new DelegateCommand(() => Task.Run(() =>
                    dispatcher.Invoke(() =>
                    {
                        DriveInfoCollection?.Clear();
                        DriveInfoCollection?.AddRange(driveCollection);
                    })));
            ChangeCollection.Execute();
        }



        public DriveInfoViewModel(IEventAggregator ea)
        {
            _ea = ea;

            CollectDrives driveCollection = new CollectDrives();

            DriveInfoCollection.AddRange(driveCollection.GetDrives());

            _ea.GetEvent<CollectionChangedEvent>().Subscribe(CollectionReceived);
        }


    }
}
