using ModuleLeft.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleLeft
{
    public class ModuleLeftModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("LeftRegion", typeof(DriveInfoView));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}