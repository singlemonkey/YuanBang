using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        public ActionResult Order()
        {
            return View();
        }

        public ActionResult Suggestion()
        {
            return View();
        }

        public ActionResult Service()
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
            if (notice == null)
            {
                return View("NotFound");
            }
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

        public ActionResult Proxy()
        {
            return View();
        }

        public ActionResult PriceProtect()
        {
            return View();
        }

        public ActionResult Collection()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }


        public JsonResult GetOrder(string order)
        {
            string url = "http://t800.chemanman.com/api/Order/Search/searchByAuth?auth=999028872cfff7ae8ee330a33cbd3874&search_num=" + order;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string str = reader.ReadToEnd();

            reader.Close();
            stream.Close();

            return Json(str, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddOrder(Order order)
        {
            JsonData json = new JsonData();
            try
            {
                order.CreateTime = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();

                json.State = true;
                json.Message = "添加成功";
            }
            catch (Exception ex) {
                json.State = false;
                json.Message = ex.Message;
            }

            return Json(json,JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddAdvice(Advice advice)
        {
            JsonData json = new JsonData();
            try
            {
                advice.CreateTime = DateTime.Now;
                db.Advices.Add(advice);
                db.SaveChanges();

                json.State = true;
                json.Message = "添加成功";
            }
            catch (Exception ex)
            {
                json.State = false;
                json.Message = ex.Message;
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}