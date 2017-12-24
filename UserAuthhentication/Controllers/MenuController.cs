using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserAuthhentication.BLL;
using UserAuthhentication.Models;

namespace UserAuthhentication.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Menu
        public ActionResult UserMenu()
        {
            IEnumerable<Menu> listMainMenu = MenuManager.GetAllMenu();
            if (listMainMenu != null)
            {
                var mainMenuList = new SelectList(listMainMenu, "MenuId", "MenuText", "");
                Session["RootMenuList"] = listMainMenu;
                ViewData["vdMainMenu"] = mainMenuList;
            }
            IEnumerable<UsersRegistration> listUsers = UserManager.GetAllUsers();
            if (listUsers != null)
            {
                var userList = new SelectList(listUsers, "UserId", "UserName", "");
                ViewData["vdUserList"] = userList;
            }
            return View();
        }
        [HttpGet]
        public JsonResult GetAllMenu()
        {
            List<Menu> menuList = new List<Menu>();
            menuList = MenuManager.GetAllMenu();
            return Json(menuList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetMenuByCode(long menuId)
        {
            var menuCode = "";
            List<Menu> menuLists = (List<Menu>)Session["RootMenuList"];
            if (menuLists != null)
            {
                menuLists = menuLists.Where(m => m.MenuId == menuId).Take(1).ToList();
                menuCode = menuLists[0].MenuCode;
            }
            List<Menu> menuList = new List<Menu>();
            menuList = MenuManager.GetMenuByCode(menuCode);
            return Json(menuList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CheckUserMenu(long userId, long menuId)
        {
            var menuList = MenuManager.UserMenuPermission(userId, menuId);

            return Json(menuList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public Int32 SaveUserMenu(Menu menu)
        {
            int success = 0;
            //if (menu.UserMenuList != null)
            //{
            //    foreach (var item in menu.UserMenuList)
            //    {
            //UserMenu userMenu = new UserMenu();
           
            //        userMenu.UserId = item.UserId;
            //        userMenu.UserMenuId = item.UserMenuId;
            //        userMenu.Permission = item.Permission;
            if (MenuManager.SaveUserMenu(menu) > 0)
                success = 1;
            else
                success = 0;
            //    }
            //}
            return success;
        }

    }
}