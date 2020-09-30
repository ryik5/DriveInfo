using ModuleBackground.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleBackground
{
    public class ModuleBackgroundModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("BackgroundRegion", typeof(Views.ModuleBackground));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

    }
}