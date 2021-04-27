using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTaskForNetDeveloperPosition.Bll.Contract;
using TestTaskForNetDeveloperPosition.Infrastructure.Contract;
using TestTaskForNetDeveloperPosition.Models;

namespace TestTaskForNetDeveloperPosition.Bll
{
    public class Output : IOutput
    {
        private readonly IUnitOfWork _unitOfWork;

        public Output(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<JoinResultModel> JoinTableGroup(IEnumerable<JoinResultModel> joinResultModels)
        {
            return joinResultModels.GroupBy(x => x.IdSitemap)
                                .Select(y => new JoinResultModel
                                {
                                    IdSitemap = y.First().IdSitemap,
                                    ArchiveOfRequestsId = y.First().ArchiveOfRequestsId,
                                    NameSite = y.First().NameSite,
                                    ElapsedMin = y.Min(x => x.Elapsed),
                                    ElapsedMax = y.Max(x => x.Elapsed)
                                }).OrderBy(o => o.ElapsedMax);
        }

        public IEnumerable<JoinResultModel> JoinTable(int id)
        {
            var siteMapResult = _unitOfWork.SitemapUnitOFWork.QueryObjectGraph(x => x.ArchiveOfRequestsId == id);
            var pageInfoResult = _unitOfWork.PageInfoUnitOFWork.Get();

            var resultJoin = (from t1 in siteMapResult
                              join t2 in pageInfoResult on t1.IdSitemap equals t2.SitemapId
                              select new JoinResultModel()
                              {
                                  IdSitemap = t1.IdSitemap,
                                  ArchiveOfRequestsId = t1.ArchiveOfRequestsId,
                                  NameSite = t1.NameSite,
                                  StatusCode = t2.StatusCode,
                                  WebsiteLoadingSpeed = t2.WebsiteLoadingSpeed,
                                  PageTestDate = t2.PageTestDate.Value.Date,
                                  Elapsed = t2.Elapsed,
                                  LastModified = t2.LastModified                                  
                              });

            return resultJoin.AsEnumerable();

        }

        //public List<string> DateOfCustomRequests(int idRequests, DateTime? date)
        //{
        //    if (date != null)
        //    {
        //        var result = _
        //    }
        //    else
        //    { 
            
        //    }
        //    var result = _unitOfWork
        //}
    }
}