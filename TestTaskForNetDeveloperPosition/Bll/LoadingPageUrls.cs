using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTaskForNetDeveloperPosition.Bll.Contract;

namespace TestTaskForNetDeveloperPosition.Bll
{
    public class LoadingPageUrls : ILoadingPageUrls
    {
        private readonly ILinkCheck _linkCheck;

        public LoadingPageUrls(
            ILinkCheck linkCheck)
        {
            _linkCheck = linkCheck;
        }

        public List<string> ExtractHref(string URL, int countLink)
        {
            var linksResult = SearchForLinks(URL);
            int i = 0;

            do
            {
                //if (countLink == i++)
                //    break;

                int indexList = 1 + i;
                string selectUriList = linksResult[indexList];

                if (_linkCheck.UrlValidation(selectUriList))
                {
                    linksResult.AddRange(SearchForLinks(linksResult[indexList]));
                }
                else
                {
                    linksResult.RemoveAt(indexList);
                }
            } while (countLink >= linksResult.Count());

            var result = linksResult.Distinct().ToList();

            return linksResult.OrderBy(x => x.Length).ToList();
        }

        private List<string> SearchForLinks(string url)
        {
            var urlList = new List<string>();
            if (urlList.Contains(url) == false)
                urlList.Add(url);

            try
            {
                var doc = new HtmlWeb().Load(url);
                var linkTags = doc.DocumentNode.Descendants("link");
                var linkedPages = doc.DocumentNode.Descendants("a")
                                                  .Select(a => a.GetAttributeValue("href", null))
                                                  .Where(u => !String.IsNullOrEmpty(u));
                string http = "";
                if (url.Contains("http://"))
                {
                    http = "http://";
                }
                else
                {
                    http = "https://";
                }

                string urls = url.Replace(http, string.Empty);
                string result = urls.Trim(new char[] { '/', ':' });

                foreach (var item in linkedPages)
                {
                    if (urlList.Any(x => x.Contains(item)) == false)
                    {
                        if (item.Contains(url))
                        {
                            urlList.Add(item);
                        }
                        else if (item.Contains(result) && item.Contains("//"))
                        {
                            urlList.Add($"{http}{item.Replace("//", string.Empty)}");
                        }
                        else if (item.StartsWith("http://") == false && item.StartsWith("https://") == false)
                        {
                            urlList.Add(url + item.TrimStart(new char[] { '/' }));
                        }
                    }

                }

                return urlList.OrderBy(x => x.Length).ToList();
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}