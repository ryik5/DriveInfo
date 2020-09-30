using Main.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication

    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleTop.ModuleTopModule>();
            moduleCatalog.AddModule<ModuleLeft.ModuleLeftModule>();
            moduleCatalog.AddModule<ModuleRight.ModuleRightModule>();
            moduleCatalog.AddModule<ModuleBackground.ModuleBackgroundModule>();
        }

    }
}
