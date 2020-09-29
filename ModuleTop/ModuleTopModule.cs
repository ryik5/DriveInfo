using ModuleTop.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleTop
{
    public class ModuleTopModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TopRegion", typeof(MessageView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}