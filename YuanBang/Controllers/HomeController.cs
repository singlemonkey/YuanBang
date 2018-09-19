using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuanBang.Models;

namespace YuanBang.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private YuanBangContext db = new YuanBangContext();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Query()
        {
            return View();
        }

        /// <summary>
        /// 路线介绍
        /// </summary>
        /// <returns></returns>
        public ActionResult Route()
        {
            return View();
        }

        public ActionResult RouteLine(string destination)
        {
            ViewBag.destination = destination;
            return View();
        }

        /// <summary>
        /// 第三方物流
        /// </summary>
        /// <returns></returns>
        public ActionResult Third()
        {
            return View();
        }

        public ActionResult Join()
        {
            return View();
        }

        public ActionResult NewsDetail(int ID)
        {
            Notice notice = db.Notices.Find(ID);
            return View(notice);
        }

        public ActionResult News()
        {
            var notices = from notice in db.Notices
                          select notice;
            notices = notices.OrderByDescending(m => m.Date).Take(20);
            return View(notices);
        }

        public ActionResult Point()
        {
            return View();
        }
    }
}