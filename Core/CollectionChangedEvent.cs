using Core.Models;
using Prism.Events;
using System.Collections.ObjectModel;

namespace Core
{
    public class CollectionChangedEvent : PubSubEvent<ObservableCollection<DriveInfoModel>>
    {
    }
}