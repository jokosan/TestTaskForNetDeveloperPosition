//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestTaskForNetDeveloperPosition
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestTaskForNetDeveloperPositionEntities : DbContext
    {
        public TestTaskForNetDeveloperPositionEntities()
            : base("name=TestTaskForNetDeveloperPositionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ArchiveOfRequests> ArchiveOfRequests { get; set; }
        public virtual DbSet<PageInfo> PageInfo { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UrlSiteMap> UrlSiteMap { get; set; }
    }
}
