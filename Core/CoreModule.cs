using Core.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Core.Models;

namespace Core
{
    public class CoreModule : PubSubEvent<DriveInfoModel>
    {
    }
}