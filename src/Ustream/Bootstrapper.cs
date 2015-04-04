using Nancy;
using Nancy.Bootstrapper;
using Nancy.ViewEngines;

namespace Ustream
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public Bootstrapper()
        {
            ResourceViewLocationProvider.RootNamespaces.Add(typeof(Bootstrapper).Assembly, "Ustream.Views");
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(cfg => cfg.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }
    }
}