using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using YuanBang.Models;
using PagedList;
using System.Data.Entity;

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

        public ActionResult Password()
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

        public ActionResult Advices()
        {
            return View();
        }

        public ActionResult AdviceDetail(int ID)
        {
            Advice advice = db.Advices.Find(ID);
            return View(advice);
        }

        public ActionResult Orders()
        {
            return View();
        }
        #endregion

        #region 用户相关方法

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
            try
            {
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
                    if (admin.ErrorTimes >= 10)
                    {
                        //如果账号不存在
                        data.State = false;
                        data.Message = "账号输错密码过多已被自动锁定，请稍后重试";
                    }
                    else
                    {
                        if (admin.Password != password)
                        {
                            //如果密码不正确
                            data.State = false;
                            data.Message = "密码不正确";

                            if (admin.FirstDateTime == null)
                            {
                                admin.FirstDateTime = DateTime.Now;
                            }
                            else
                            {
                                DateTime prevDateTime = DateTime.Parse(admin.FirstDateTime.ToString());
                                //如果没有超过十分钟
                                if (prevDateTime.AddMinutes(10) > DateTime.Now)
                                {
                                    admin.ErrorTimes += 1;
                                    db.Entry(admin).State = EntityState.Modified;
                                }
                                else
                                {
                                    admin.FirstDateTime = DateTime.Now;
                                    admin.ErrorTimes = 1;
                                }

                            }
                            db.SaveChanges();
                        }
                        else
                        {
                            //保存session信息
                            data.State = true;
                            data.Message = "登陆成功";

                            Session["Admin"] = admin.UserName;
                        }
                    }

                }

                return Json(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public JsonResult ModifyPassword(string prevPassword,string newPassword)
        {
            JsonData json = new JsonData();
            try
            {
                string userName = Session["Admin"].ToString();
                Admin admin = db.Admins.Single(m => m.UserName == userName);
                if (admin.Password != prevPassword)
                {
                    json.State = false;
                    json.Message = "原密码输入错误，请重新输入";
                }
                else
                {
                    admin.Password = newPassword;
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();

                    json.State = true;
                    json.Message = "修改成功";
                }
            }
            catch (Exception ex)
            {
                json.State = false;
                json.Message = ex.Message;
            }
            return Json(json,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 新闻相关方法
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

        public JsonResult DeleteNotices(IList<int> IDs)
        {
            JsonData json = new JsonData();

            try
            {
                foreach (var id in IDs)
                {
                    Notice notice = db.Notices.Find(id);
                    db.Entry(notice).State = EntityState.Deleted;
                }
                db.SaveChanges();

                json.State = true;
                json.Message = "删除成功";
            }
            catch (Exception ex)
            {
                json.State = false;
                json.Message = ex.Message;
            }

            return Json(json);
        }

        [HttpPost]
        public JsonResult EditNotice(Notice notice)
        {
            JsonData json = new JsonData();
            try
            {
                notice.Date = DateTime.Now;
                db.Entry(notice).State = EntityState.Modified;
                db.SaveChanges();
                json.State = true;
                json.Message = "保存成功";
            }
            catch (Exception ex)
            {
                json.State = false;
                json.Message = ex.Message;
            }

            return Json(json);
        }
        #endregion

        #region 投诉建议相关方法

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetAdvices(AdvicesQueryInfo queryInfo, PageInfo pageInfo)
        {
            var advices = from advice in db.Advices
                          select advice;

            if (queryInfo.Type != null)
            {
                advices = advices.Where(m => m.Type == queryInfo.Type);
            }
            if (queryInfo.Title != null && queryInfo.Title.Trim().Length != 0)
            {
                advices = advices.Where(m => m.Title.Contains(queryInfo.Title));
            }

            int count = advices.Count();

            return Json(new
            {
                count = count,
                list = advices.OrderByDescending(m => m.CreateTime).ToPagedList(pageInfo.PageIndex, pageInfo.PageSize)
            });
        }

        public JsonResult DeleteAdvices(IList<int> IDs)
        {
            JsonData json = new JsonData();

            try
            {
                foreach (var id in IDs)
                {
                    Advice advice = db.Advices.Find(id);
                    db.Entry(advice).State = EntityState.Deleted;
                }
                db.SaveChanges();

                json.State = true;
                json.Message = "删除成功";
            }
            catch (Exception ex)
            {
                json.State = false;
                json.Message = ex.Message;
            }

            return Json(json);
        }

        #endregion

        #region 订单相关方法

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetOrders(Object queryInfo, PageInfo pageInfo)
        {
            var orders = from order in db.Orders
                         select order;

            int count = orders.Count();

            return Json(new
            {
                count = count,
                list = orders.OrderByDescending(m => m.CreateTime).ToPagedList(pageInfo.PageIndex, pageInfo.PageSize)
            });
        }

        public JsonResult DeleteOrders(IList<int> IDs)
        {
            JsonData json = new JsonData();

            try
            {
                foreach (var id in IDs)
                {
                    Order order = db.Orders.Find(id);
                    db.Entry(order).State = EntityState.Deleted;
                }
                db.SaveChanges();

                json.State = true;
                json.Message = "删除成功";
            }
            catch (Exception ex)
            {
                json.State = false;
                json.Message = ex.Message;
            }

            return Json(json);
        }

        #endregion

        #region 字典相关方法
        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetDictionaries(string dictionaryType)
        {
            int parentID = db.Dictionaries.First(m => m.Name == dictionaryType && m.Level == 0).ID;
            List<Dictionary> dictionaries = db.Dictionaries.Where(m => m.ParentID == parentID).OrderBy(m => m.DisPlayIndex).ToList();
            return Json(dictionaries);
        }

        #endregion

    }
}