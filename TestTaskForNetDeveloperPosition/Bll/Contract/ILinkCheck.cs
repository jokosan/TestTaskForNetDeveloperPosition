using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskForNetDeveloperPosition.Bll.Contract
{
    public interface ILinkCheck
    {
        string AddressHost(string address);
        bool UrlValidation(string address);
    }
}
