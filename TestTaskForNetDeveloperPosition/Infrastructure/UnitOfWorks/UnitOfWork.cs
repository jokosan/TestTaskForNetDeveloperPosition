using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TestTaskForNetDeveloperPosition.Infrastructure.Contract;

namespace TestTaskForNetDeveloperPosition.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        internal TestTaskForNetDeveloperPositionEntities _entities;

        public UnitOfWork()
        {
            _entities = new TestTaskForNetDeveloperPositionEntities();
        }

        private DbRepository<ArchiveOfRequests> ArchiveOfRequestsUW;
        private DbRepository<UrlSiteMap> SitemapUW;
        private DbRepository<PageInfo> PageInfoUW;

        public DbRepository<ArchiveOfRequests> ArchiveOfRequestsUnitOfWork
        {
            get => ArchiveOfRequestsUW ?? (ArchiveOfRequestsUW = new DbRepository<ArchiveOfRequests>(_entities));
            set => ArchiveOfRequestsUW = value;
        }

        public DbRepository<UrlSiteMap> SitemapUnitOFWork
        {
            get => SitemapUW ?? (SitemapUW = new DbRepository<UrlSiteMap>(_entities));
            set => SitemapUW = value;
        }

        public DbRepository<PageInfo> PageInfoUnitOFWork
        {
            get => PageInfoUW ?? (PageInfoUW = new DbRepository<PageInfo>(_entities));
            set => PageInfoUW = value;
        }

        #region Dispose
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Save()
        {
            _entities.SaveChanges();
        }


        #endregion
    }
}