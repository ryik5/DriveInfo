using ModuleRight.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleRight
{
    public class ModuleRightModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("RightRegion", typeof(DriveInfo));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}