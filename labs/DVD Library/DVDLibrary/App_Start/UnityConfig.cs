using DVDLibrary.Data.Interfaces;
using DVDLibrary.Data.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Configuration;
using System.Web.Http;
using Unity.WebApi;

namespace DVDLibrary
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            string repositoryType = ConfigurationManager.AppSettings["RepositoryType"];

            if (repositoryType == "Mock")
            {
                container.RegisterType<IDvdRepository, MockRepository>();
            }
            else if (repositoryType == "ADO")
            {
                container.RegisterType<IDvdRepository, ADORepository>();
            }
            else if (repositoryType == "DP")
            {
                container.RegisterType<IDvdRepository, DPRepository>();
            }
            else if (repositoryType == "EF")
            {
                container.RegisterType<IDvdRepository, EFRepository>();
            }
            else
            {
                throw new Exception("Repository type in app.config not set properly.");
            }

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}