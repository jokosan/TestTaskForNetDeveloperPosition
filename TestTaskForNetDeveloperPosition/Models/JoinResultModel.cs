using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTaskForNetDeveloperPosition.Models
{
    public class JoinResultModel
    {
        [Display(Name = "id URL")]
        public int IdSitemap { get; set; }
        [Display(Name = "id User request")]
        public Nullable<int> ArchiveOfRequestsId { get; set; }
        [Display(Name = "Name")]
        public string NameSite { get; set; }
        public int IdPageInfo { get; set; }
        public Nullable<long> WebsiteLoadingSpeed { get; set; }
        [Display(Name = "Status code")]
        public Nullable<int> StatusCode { get; set; }
        [Display (Name = "Data request")]
        public Nullable<System.DateTime> PageTestDate { get; set; }
        [Display(Name = "Last modified")]
        public Nullable<System.DateTime> LastModified { get; set; }
        [Display(Name = "Request time")]
        public Nullable<System.TimeSpan> Elapsed { get; set; }

        [Display(Name = "Request time min")]
        public Nullable<System.TimeSpan> ElapsedMin { get; set; }
        [Display(Name = "Request time max")]
        public Nullable<System.TimeSpan> ElapsedMax { get; set; }
    }
}