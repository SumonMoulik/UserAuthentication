using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UserAuthhentication.BLL;

namespace UserAuthhentication.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public BaseController()
        {

        }
        protected override void Initialize(RequestContext requestContext)
        {
            try
            {
                base.Initialize(requestContext);
                if (Session["Email"] != null)
                {
                    ViewBag.Menu = LoginManager.UserMenu(Convert.ToInt32(Session["UserId"])).ToList();
                    Session["UserMenu"] = ViewBag.Menu;
                }
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }
    }
}