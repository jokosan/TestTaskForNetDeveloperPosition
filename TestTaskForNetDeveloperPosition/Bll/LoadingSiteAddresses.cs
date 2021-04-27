using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TestTaskForNetDeveloperPosition.Bll.Contract;
using TestTaskForNetDeveloperPosition.Infrastructure.Contract;
using TestTaskForNetDeveloperPosition.Models;

namespace TestTaskForNetDeveloperPosition.Bll
{
    public class LoadingSiteAddresses : ILoadingSiteAddresses
    {
        private readonly ILinkCheck _linkCheck;
        private readonly ILoadingPageUrls _loadingPageUrls;
        private readonly IWebsiteLoadingSpeed _websiteLoadingSpeed;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOutput _output;

        public LoadingSiteAddresses(
            ILinkCheck linkCheck,
            ILoadingPageUrls loadingPageUrls,
            IWebsiteLoadingSpeed websiteLoadingSpeed,
            IUnitOfWork unitOfWork,
            IOutput output)
        {
            _linkCheck = linkCheck;
            _loadingPageUrls = loadingPageUrls;
            _websiteLoadingSpeed = websiteLoadingSpeed;
            _unitOfWork = unitOfWork;
            _output = output;
        }

        public int SaveUserRequest(string url)
        {
            var resultArxiv = _unitOfWork.ArchiveOfRequestsUnitOfWork.Get();

            if (resultArxiv.Any(x => x.NameUrl.Contains(url)))
            {
                var resultId = resultArxiv.FirstOrDefault(x => x.NameUrl.Contains(url));
                return resultId.IdArchiveOfRequests;
            }
            else
            {
                var archive = new ArchiveOfRequests();
                archive.NameUrl = url;
                _unitOfWork.ArchiveOfRequestsUnitOfWork.Insert(archive);
                _unitOfWork.Save();

                return archive.IdArchiveOfRequests;
            }
        }

        public void Loading(string url, int numberOfLinks, int IdUri)
        {
            _websiteLoadingSpeed.SpeedPageUploads(_loadingPageUrls.ExtractHref(url, numberOfLinks), IdUri);
        }

        public bool ValidationAddresses(string url)
            => _linkCheck.UrlValidation(_linkCheck.AddressHost(url));

        public IEnumerable<JoinResultModel> GetSitemaps(int id)
            => _output.JoinTableGroup(_output.JoinTable(id));

        public IEnumerable<ArchiveOfRequests> Arxiv()
            => _unitOfWork.ArchiveOfRequestsUnitOfWork.Get();

        public IEnumerable<JoinResultModel> Arxiv(int id)
            => _output.JoinTable(id).OrderBy(x => x.PageTestDate);

        public IEnumerable<JoinResultModel> Arxiv(int id, DateTime date)
            => _output.JoinTable(id).Where(w => w.PageTestDate.Value.Date == date.Date).OrderBy(x => x.PageTestDate);
    }
}