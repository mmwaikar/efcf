using Common.DataAccess;
using Entities.Common;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Actual.WebApi
{
    public static class UnityConfig
    {
        public static UnityContainer Container { get; set; }
        
        public static void RegisterComponents()
        {
            Container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            Container
                .RegisterType<IUnitOfWork, UnitOfWork>();

            Container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Hierarchical);
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(Container);
        }
    }
}