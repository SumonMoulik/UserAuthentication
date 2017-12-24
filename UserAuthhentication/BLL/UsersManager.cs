using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserAuthhentication.DAL;
using UserAuthhentication.Models;

namespace UserAuthhentication.BLL
{
    public static class LoginManager
    {
        public static List<UsersRegistration> UserLogin()
        {
            return LoginGateway.UserLogin();
        }
        public static List<Menu> UserMenu(long userId)
        {
            return LoginGateway.UserMenu(userId);
        }

    }
    public static class MenuManager
    {
        public static List<Menu> GetAllMenu()
        {
            return MenuGateWay.GetAllMenu();
        }
        public static List<Menu> GetMenuByCode(string menuCode)
        {
            return MenuGateWay.GetMenuByCode(menuCode);
        }
        public static UserMenu UserMenuPermission(long userId, long menuId)
        {
            return MenuGateWay.UserMenuPermission(userId,menuId);
        }
        public static Int32 SaveUserMenu(Menu userMenu)
        {
            return MenuGateWay.SaveUserMenu(userMenu);
        }
    }
    public static class UserManager
    {
        public static List<UsersRegistration> GetAllUsers()
        {
            return UserGateWay.GetAllUsers();
        }
        public static List<UsersRegistration> GetUsersList()
        {
            return UserGateWay.GetUsersList();
        }
        public static Int32 SaveUser(UsersRegistration user)
        {
            return UserGateWay.SaveUser(user);
        }
    }
}