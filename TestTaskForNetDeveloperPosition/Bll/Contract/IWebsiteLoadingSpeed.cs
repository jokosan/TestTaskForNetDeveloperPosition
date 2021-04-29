using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskForNetDeveloperPosition.Bll.Contract
{
    public interface IWebsiteLoadingSpeed
    {
        List<string> SpeedPageUploads(List<string> url, int idUrl);
    }
}
