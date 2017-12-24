using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserAuthhentication.BLL;
using UserAuthhentication.Models;

namespace UserAuthhentication.Controllers.Users
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult User()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUsersList()
        {
            List<UsersRegistration> usersList = UserManager.GetUsersList();
            return Json(usersList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public Int32 SaveUser(UsersRegistration users)
        {
            try
            {
                int success = 0;
                UsersRegistration user = new UsersRegistration();
                user.UserId = users.UserId;
                user.UserName = users.UserName;
                user.UserEmail = users.UserEmail;
                user.Password = users.Password;
                if (UserManager.SaveUser(users) > 0)
                    success = 1;
                else
                    success = 0;
                return success;
            }
            catch (Exception excp)
            {
                throw excp;
            }            
        }
    }
}