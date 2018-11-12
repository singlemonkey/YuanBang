using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuanBang.Models
{
    public class JsonData
    {
        public bool State { get; set; }
        public string Message { get; set; }
    }

    public class PageInfo
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }



    public class NewsQueryInfo
    {
        public int? Type { get; set; }
        public string Name { get; set; }
    }

    public class AdvicesQueryInfo
    {
        public string Type { get; set; }
        public string Title { get; set; }
    }
}