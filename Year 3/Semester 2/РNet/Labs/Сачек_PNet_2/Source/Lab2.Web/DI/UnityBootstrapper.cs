using Unity;
using Unity.AspNet.Mvc;
using System.Web.Mvc;
using Lab2.Web.Database;
using System.Configuration;

namespace Lab2.Web.DI
{
    public static class UnityBootstrapper
    {
        public static void Initialize()
        {
            var container = new UnityContainer();

            container.RegisterInstance(typeof(DbContext), new DbContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}