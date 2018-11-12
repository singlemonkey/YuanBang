using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using YuanBang.Models;

namespace YuanBang.Controllers
{
    [AllowAnonymous]
    public class MobileController : Controller
    {
        private YuanBangContext db = new YuanBangContext();
        // GET: Mobile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Query()
        {
            return View();
        }

        public ActionResult Route()
        {
            return View();
        }

        public ActionResult Order()
        {
            return View();
        }

        public ActionResult Logistics()
        {
            return View();
        }

        public ActionResult Company()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Suggestion()
        {
            return View();
        }

        public ActionResult GzToXa()
        {
            return View();
        }

        public ActionResult Service()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult NewsDetail(int ID)
        {
            Notice notice = db.Notices.Find(ID);
            return View(notice);
        }
    }
}