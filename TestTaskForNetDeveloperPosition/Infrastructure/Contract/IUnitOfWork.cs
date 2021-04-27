using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskForNetDeveloperPosition.Infrastructure.Contract
{
    public interface IUnitOfWork
    {
        void Dispose();

        void Save();

        DbRepository<ArchiveOfRequests> ArchiveOfRequestsUnitOfWork { get; set; }
        DbRepository<UrlSiteMap> SitemapUnitOFWork { get; set; }
        DbRepository<PageInfo> PageInfoUnitOFWork { get; set; }
    }
}
