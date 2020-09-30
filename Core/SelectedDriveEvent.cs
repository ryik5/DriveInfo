using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Core.Models;

namespace Core
{
    public class SelectedDriveEvent : PubSubEvent<DriveInfoModel>
    {
    }
}