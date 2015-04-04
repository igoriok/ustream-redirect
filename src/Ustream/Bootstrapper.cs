using Nancy;
using Nancy.Bootstrapper;
using Nancy.ViewEngines;

namespace Ustream
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public Bootstrapper()
        {
            StaticConfiguration.DisableErrorTraces = false;
            ResourceViewLocationProvider.RootNamespaces.Add(typeof(Bootstrapper).Assembly, "Ustream.Views");
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(cfg => cfg.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }
    }
}