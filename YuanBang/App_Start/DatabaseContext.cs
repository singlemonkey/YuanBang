using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using YuanBang.Models;

namespace YuanBang
{
    public class YuanBangContext : DbContext
    {
        public YuanBangContext() : base("YuanBangContext")
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<Notice> Notices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    /// <summary>
    /// 初始化数据库数据
    /// </summary>
    public class ModelInitializer :DropCreateDatabaseIfModelChanges<YuanBangContext>
    {
        protected override void Seed(YuanBangContext context)
        {
            //生成字典表
            var dictionaries = new List<Dictionary>
            {
                new Dictionary{ID =1,DisPlayIndex =0,Level =0,Name ="新闻公告",ParentID =0},
                new Dictionary{ID =2,DisPlayIndex =0,Level =1,Name ="新闻",ParentID =1},
                new Dictionary{ID =3,DisPlayIndex =1,Level =1,Name ="公告",ParentID =1}
            };
            dictionaries.ForEach(d => context.Dictionaries.Add(d));
            context.SaveChanges();

            var admins = new List<Admin>
            {
                new Admin {ID = 1,UserName = "admin",Password = "123456"},
                new Admin {ID = 2,UserName = "superadmin",Password = "123456"}
            };
            admins.ForEach(a => context.Admins.Add(a));
            context.SaveChanges();

            var notices = new List<Notice>
            {
                new Notice{ID = 1,NoticeTypeID =2,Date =DateTime.Parse("2018-08-08"),Title ="新闻内容",Content ="这是一条新的新闻，没有内容"},
                new Notice{ID =2,NoticeTypeID =3,Date =DateTime.Parse("2018-09-08"),Title ="另一条新闻内容",Content ="这是一条新的新闻，没有内容"}
            };
            notices.ForEach(n => context.Notices.Add(n));
            context.SaveChanges();
        }
    }
}