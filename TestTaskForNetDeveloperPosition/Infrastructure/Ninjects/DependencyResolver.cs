using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTaskForNetDeveloperPosition.Bll;
using TestTaskForNetDeveloperPosition.Bll.Contract;
using TestTaskForNetDeveloperPosition.Infrastructure.Contract;
using TestTaskForNetDeveloperPosition.Infrastructure.UnitOfWorks;

namespace TestTaskForNetDeveloperPosition.Infrastructure.Ninjects
{
    public class DependencyResolver
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<ILoadingSiteAddresses>().To<LoadingSiteAddresses>();
            kernel.Bind<ILinkCheck>().To<LinkCheck>();
            kernel.Bind<ILoadingPageUrls>().To<LoadingPageUrls>();
            kernel.Bind<IWebsiteLoadingSpeed>().To<WebsiteLoadingSpeed>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IOutput>().To<Output>();

        }
    }
}