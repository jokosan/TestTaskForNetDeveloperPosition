using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using TestTaskForNetDeveloperPosition.Bll.Contract;
using TestTaskForNetDeveloperPosition.Infrastructure;
using TestTaskForNetDeveloperPosition.Infrastructure.Contract;

namespace TestTaskForNetDeveloperPosition.Bll
{
    public class WebsiteLoadingSpeed : IWebsiteLoadingSpeed
    {
        private readonly IUnitOfWork _unitOfWork;

        public WebsiteLoadingSpeed(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SpeedPageUploads(List<string> url, int IdUrl)
        {
            var sitemap = new UrlSiteMap();
            var pageInfo = new PageInfo();
            var sitmapResult = _unitOfWork.SitemapUnitOFWork.Get();

            foreach (var item in url)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(item);

                    req.Method = WebRequestMethods.Http.Get;
                    req.AllowAutoRedirect = false;

                    req.Accept = @"*/*";

                    sw.Start();
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    var rescode = (int)res.StatusCode;
                    sw.Stop();

                    res.Close();

                    TimeSpan timeToLoad = sw.Elapsed;

                    if (sitmapResult.Any(x => x.NameSite.Contains(item)))
                    {
                        var result = sitmapResult.Where(x => x.NameSite.Contains(item));
                        var resultWhere = result.LastOrDefault();
                        pageInfo.SitemapId = resultWhere.IdSitemap;
                    }
                    else
                    {
                        sitemap.ArchiveOfRequestsId = IdUrl;
                        sitemap.NameSite = item;
                        pageInfo.SitemapId = SaveSitemap(sitemap);
                    }

                    //pageInfo.SitemapId = id;
                    pageInfo.WebsiteLoadingSpeed = sw.ElapsedTicks;
                    pageInfo.StatusCode = rescode;
                    pageInfo.PageTestDate = DateTime.Now;
                    pageInfo.LastModified = res.LastModified;
                    pageInfo.Elapsed = sw.Elapsed;

                    _unitOfWork.PageInfoUnitOFWork.Insert(pageInfo);
                    _unitOfWork.Save();
                }
                catch (WebException ex)
                {
                    if (ex.Response == null)
                        throw;

                    sitemap.ArchiveOfRequestsId = IdUrl;
                    sitemap.NameSite = item;

                    _unitOfWork.SitemapUnitOFWork.Insert(sitemap);
                    _unitOfWork.Save();

                    pageInfo.SitemapId = sitemap.IdSitemap;
                    pageInfo.StatusCode = (int)((HttpWebResponse)ex.Response).StatusCode;
                    pageInfo.PageTestDate = DateTime.Now;

                    _unitOfWork.PageInfoUnitOFWork.Insert(pageInfo);
                    _unitOfWork.Save();
                }
            }

        }
        private int SaveSitemap(UrlSiteMap row)
        {
            _unitOfWork.SitemapUnitOFWork.Insert(row);
            _unitOfWork.Save();
            return row.IdSitemap;
        }
    }
}