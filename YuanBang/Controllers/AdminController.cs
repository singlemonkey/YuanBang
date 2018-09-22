using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using YuanBang.Models;
using PagedList;

namespace YuanBang.Controllers
{
    public class AdminController : Controller
    {
        private YuanBangContext db = new YuanBangContext();

        #region 页面视图
        /// <summary>
        /// 用户登录页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 后台管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 新闻管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult News()
        {
            return View();
        }
        /// <summary>
        /// 添加新闻公告页
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNotice()
        {
            return View();
        }

        public ActionResult NoticeDetail(int ID)
        {
            Notice notice = db.Notices.Find(ID);
            return View(notice);
        }
        #endregion

        #region 相关方法

        /// <summary>
        /// 登录验证方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(string userName, string password)
        {
            try {
                JsonData data = new JsonData();

                Admin admin = db.Admins.FirstOrDefault(m => m.UserName == userName);
                if (admin == null)
                {
                    //如果账号不存在
                    data.State = false;
                    data.Message = "账号不存在";
                }
                else
                {
                    if (admin.Password != password)
                    {
                        //如果密码不正确
                        data.State = false;
                        data.Message = "密码不正确";
                    }
                    else
                    {
                        //保存session信息
                        data.State = true;
                        data.Message = "登陆成功";

                        Session["Admin"] = admin.UserName;
                    }
                }

                return Json(data);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// 获取新闻公告信息，分页显示
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetNotices(NewsQueryInfo queryInfo, PageInfo pageInfo)
        {
            var notices = from notice in db.Notices
                          select notice;

            if (queryInfo.Type != null)
            {
                notices = notices.Where(m => m.NoticeTypeID == queryInfo.Type);
            }
            if (queryInfo.Name != null && queryInfo.Name.Trim().Length != 0)
            {
                notices = notices.Where(m => m.Title.Contains(queryInfo.Name));
            }

            int count = notices.Count();

            return Json(new
            {
                count = count,
                list = notices.OrderByDescending(m => m.Date).ToPagedList(pageInfo.PageIndex, pageInfo.PageSize)
            });
        }

        [HttpPost]
        public JsonResult AddNotice(Notice notice)
        {
            notice.Date = DateTime.Now;
            JsonData jsonData = new JsonData();
            try
            {
                db.Notices.Add(notice);
                db.SaveChanges();

                jsonData.State = true;
                jsonData.Message = "保存成功";
            }
            catch (Exception ex)
            {
                jsonData.State = false;
                jsonData.Message = ex.Message;
            }
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditNotice(int ID)
        {
            Notice notice = db.Notices.Find(ID);
            return View(notice);
        }

        [HttpPost]
        public ActionResult EditNotice(Notice notice)
        {
            notice.Date = DateTime.Now;
            return Json(new { });
        }
        #endregion

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetDictionaries(string dictionaryType)
        {
            int parentID = db.Dictionaries.First(m => m.Name == dictionaryType && m.Level == 0).ID;
            List<Dictionary> dictionaries = db.Dictionaries.Where(m => m.ParentID == parentID).OrderBy(m => m.DisPlayIndex).ToList();
            return Json(dictionaries);
        }
    }
}