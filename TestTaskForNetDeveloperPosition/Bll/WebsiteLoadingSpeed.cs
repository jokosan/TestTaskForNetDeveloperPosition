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

        public List<string> SpeedPageUploads(List<string> url, int IdUrl)
        {
            var sitemap = new UrlSiteMap();
            var pageInfo = new PageInfo();
            var listWebExceptionResponse = new List<string>();
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
                        pageInfo.SitemapId = SaveTableSiteMap(sitmapResult, item);
                    }
                    else
                    {
                        sitemap.ArchiveOfRequestsId = IdUrl;
                        sitemap.NameSite = item;
                        pageInfo.SitemapId = SaveSitemap(sitemap);
                    }

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
                    {
                        // если делать правильно я бы поставил Nlog и ошибки записывал в бд или в файл 
                        // хотя и пользователю есть смысл вывести такого рода исключения при условии, что битая ссылка непосредственно находится на сайте
                        // были ещё идеи такие как проверка домена с помощью регулярных выражений, но отказался 
                                                
                        listWebExceptionResponse.Add(ex.Message);
                    }
                    else
                    {
                        if (sitmapResult.Any(x => x.NameSite.Contains(item)))
                        {
                            pageInfo.SitemapId = SaveTableSiteMap(sitmapResult, item);
                        }
                        else
                        {
                            sitemap.ArchiveOfRequestsId = IdUrl;
                            sitemap.NameSite = item;
                            pageInfo.SitemapId = SaveSitemap(sitemap);
                        }

                        pageInfo.StatusCode = (int)((HttpWebResponse)ex.Response).StatusCode;
                        pageInfo.PageTestDate = DateTime.Now;

                        _unitOfWork.PageInfoUnitOFWork.Insert(pageInfo);
                        _unitOfWork.Save();
                    }
                }
            }

            return listWebExceptionResponse;
        }

        private int SaveTableSiteMap(IEnumerable<UrlSiteMap> urlSiteMaps, string item)
        {
            var result = urlSiteMaps.Where(x => x.NameSite.Contains(item));
            var resultWhere = result.LastOrDefault();
            return resultWhere.IdSitemap;
        }

        private int SaveSitemap(UrlSiteMap row)
        {
            _unitOfWork.SitemapUnitOFWork.Insert(row);
            _unitOfWork.Save();
            return row.IdSitemap;
        }
    }
}