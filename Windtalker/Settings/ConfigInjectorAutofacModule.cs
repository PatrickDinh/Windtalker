using Autofac;
using ConfigInjector.Configuration;

namespace Windtalker.Settings
{
    public class ConfigInjectorAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationConfigurator.RegisterConfigurationSettings()
                .FromAssemblies(ThisAssembly)
                .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                    .AsSelf()
                    .SingleInstance())
                .AllowConfigurationEntriesThatDoNotHaveSettingsClasses(true)
                .DoYourThing();
        }
    }
}