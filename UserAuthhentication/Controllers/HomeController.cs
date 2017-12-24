using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserAuthhentication.BLL;
using UserAuthhentication.Models;

namespace UserAuthhentication.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            try
            {
                if (string.IsNullOrEmpty(Session["Email"].ToString()) && string.IsNullOrEmpty(Session["Password"].ToString()))
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.Menu = LoginManager.UserMenu(Convert.ToInt32(Session["UserId"])).ToList();
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Home");
            }

        }
        public ActionResult Login()
        {
            return View();
        }

        public int LoginInfo(string email, string password)
        {
            try
            {
                long userId = 0;
                List<UsersRegistration> userRegInfo = LoginManager.UserLogin().ToList();
                userRegInfo = userRegInfo.Where(m => m.UserEmail == email && m.Password == password).ToList();
                if (userRegInfo.Count() > 0)
                {
                    Session["Email"] = userRegInfo.FirstOrDefault().UserEmail;
                    Session["Password"] = userRegInfo.FirstOrDefault().Password;
                    Session["UserName"] = userRegInfo.FirstOrDefault().UserName;
                    Session["UserId"] = userRegInfo.FirstOrDefault().UserId;
                    userId = userRegInfo.FirstOrDefault().UserId;
                }
                return Convert.ToInt32(userId);
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }
        public ActionResult Logout()
        {
            Response.ExpiresAbsolute = DateTime.Now;
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Session["UserId"] =null;
            Session["Email"] = null;
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            return RedirectToAction("Login", "Home");
        }
    }
}