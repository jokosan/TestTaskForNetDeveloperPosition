using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskForNetDeveloperPosition.Models;

namespace TestTaskForNetDeveloperPosition.Bll.Contract
{
    public interface ILoadingSiteAddresses
    {
        int SaveUserRequest(string url);
        List<string> Loading(string url, int numberOfLinks, int IdUri);
        bool ValidationAddresses(string url);
        IEnumerable<JoinResultModel> GetSitemaps(int id);
        IEnumerable<ArchiveOfRequests> Arxiv();
        IEnumerable<JoinResultModel> Arxiv(int id);
        IEnumerable<JoinResultModel> Arxiv(int id, DateTime date);
    }
}
