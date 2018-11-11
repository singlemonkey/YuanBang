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

    public class Order
    {
        public int ID { get; set; }
        public string SendName { get; set; }
        public string SendPhoneNumber { get; set; }
        public string SendAddress { get; set; }
        public string GoodsName { get; set; }
        public int Amount { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string ReceiverAddress { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class Advice
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        [NotMapped]
        public string TypeString
        {
            get
            {
                if (Type == "complain")
                {
                    return "投诉";
                }
                else
                {
                    return "建议";
                }
            }
        }
        public string Content { get; set; }
        public string ContactPhoneNumber { get; set; }
        public DateTime CreateTime { get; set; }
        [NotMapped]
        public string DateString
        {
            get => CreateTime.ToString("yyyy-MM-dd");
        }
    }
}