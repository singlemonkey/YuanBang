using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace YuanBang
{
    public class UserAuthAttribute : AuthorizeAttribute
    {
        public bool isLogin;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool loginState = false;
            try
            {
                string userName = httpContext.Session["Admin"].ToString();
                if (userName.Trim().Length == 0)
                {
                    loginState = false;
                }
                else
                {
                    loginState = true;
                }
            }
            catch (Exception)
            {
                loginState = false;
            }
            isLogin = loginState;
            return loginState;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            else
            {
                if (!isLogin)
                {
                    filterContext.HttpContext.Response.Redirect("/Admin/Login");
                }
            }
        }
    }
}