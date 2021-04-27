using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTaskForNetDeveloperPosition.Bll.Contract
{
    public interface ILoadingPageUrls
    {
        List<string> ExtractHref(string URL, int countLink);

    }
}