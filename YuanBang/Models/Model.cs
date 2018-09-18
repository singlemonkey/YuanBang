using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace YuanBang.Models
{
    public class Admin
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Dictionary
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int ParentID { get; set; }
        public int DisPlayIndex { get; set; }
    }

    public class Notice
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int NoticeTypeID { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public string DateString
        {
            get => Date.ToString("yyyy-MM-dd");
        }
        public string Content { get; set; }

        public virtual Dictionary NoticeType { get; set; }
    }
}