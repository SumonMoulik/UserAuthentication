using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UserAuthhentication.Models;

namespace UserAuthhentication.DAL
{
    public static class LoginGateway
    {
        private static SqlConnection sqlConnection = null;
        private static SqlCommand sqlCommand = null;
        private static DataAccess dataAccess = new DataAccess();

        public static List<UsersRegistration> UserLogin()
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Select * from tblUsers";

                List<UsersRegistration> usersList = null;
                sqlConnection.Close();
                sqlCommand.Connection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    usersList = new List<UsersRegistration>();

                    while (dataReader.Read())
                    {
                        UsersRegistration users = new UsersRegistration();
                        users.UserId = Convert.ToInt32(dataReader["UserId"]);
                        users.UserName = dataReader["UserName"].ToString();
                        users.UserEmail = dataReader["Email"].ToString();
                        users.Password = dataReader["Password"].ToString();
                        usersList.Add(users);
                    }
                }
                dataReader.Close();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return usersList;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }

        public static List<Menu> UserMenu(long userId)
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Select * from vUserMenu Where UserId=" + userId + " And UserPermission=1 order by MenuOrder";

                List<Menu> userMenuList = null;
                sqlConnection.Close();
                sqlCommand.Connection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    userMenuList = new List<Menu>();

                    while (dataReader.Read())
                    {
                        Menu userMenu = new Menu();
                        userMenu.MenuId = Convert.ToInt32(dataReader["MenuId"]);
                        userMenu.MenuCode = dataReader["MenuCode"].ToString();
                        userMenu.ParentId = dataReader["ParentId"].ToString();
                        userMenu.MenuText = dataReader["MenuText"].ToString();
                        userMenu.MenuUrl = dataReader["URL"].ToString();
                        userMenu.MenuOrder = Convert.ToInt32(dataReader["MenuOrder"]);
                        userMenuList.Add(userMenu);
                    }
                }
                dataReader.Close();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return userMenuList;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }
    }

    public static class MenuGateWay
    {
        private static SqlConnection sqlConnection = null;
        private static SqlCommand sqlCommand = null;
        private static DataAccess dataAccess = new DataAccess();
        public static List<Menu> GetAllMenu()
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Select MenuId,MenuCode,MenuText,IsActive from tblMenu Where ParentId='ROOT'";

                List<Menu> menuList = null;
                sqlConnection.Close();
                sqlCommand.Connection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    menuList = new List<Menu>();

                    while (dataReader.Read())
                    {
                        Menu menu = new Menu();
                        menu.MenuId = Convert.ToInt32(dataReader["MenuId"]);
                        menu.MenuText = dataReader["MenuText"].ToString();
                        menu.MenuCode = dataReader["MenuCode"].ToString();
                        menu.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                        menuList.Add(menu);
                    }
                }
                dataReader.Close();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return menuList;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }
        public static List<Menu> GetMenuByCode(string menuCode)
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Select MenuId,MenuText,IsActive from tblMenu Where ParentId='" + menuCode + "' And IsActive=1";

                List<Menu> menuList = null;
                sqlConnection.Close();
                sqlCommand.Connection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    menuList = new List<Menu>();

                    while (dataReader.Read())
                    {
                        Menu menu = new Menu();
                        menu.MenuId = Convert.ToInt32(dataReader["MenuId"]);
                        menu.MenuText = dataReader["MenuText"].ToString();
                        menu.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                        menuList.Add(menu);
                    }
                }
                dataReader.Close();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return menuList;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }
        public static UserMenu UserMenuPermission(long userId, long menuId)
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Select * from tblPermission Where UserId=" + userId + " And MenuId=" + menuId + "";

                UserMenu userMenu = null;
                sqlConnection.Close();
                sqlCommand.Connection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        userMenu = new UserMenu();
                        userMenu.UserMenuId = Convert.ToInt32(dataReader["MenuId"]);
                        userMenu.UserId = Convert.ToInt32(dataReader["UserId"]);
                        userMenu.Permission = Convert.ToBoolean(dataReader["UserPermission"]);
                    }
                }
                dataReader.Close();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return userMenu;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }
        public static Int32 SaveUserMenu(Menu userMenu)
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "sp_UserMenu";
                sqlConnection.Close();
                sqlCommand.Connection.Open(); 

                sqlCommand.Parameters.AddWithValue("@user_menu_xml", userMenu.GetMenuPermissionXML());
                //sqlCommand.Parameters.AddWithValue("@UserId", userMenu.UserId);
                //sqlCommand.Parameters.AddWithValue("@MenuId", userMenu.UserMenuId);
                //sqlCommand.Parameters.AddWithValue("@UserPermission", userMenu.Permission);

                int result = sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return result;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }
    }

    public static class UserGateWay
    {
        private static SqlConnection sqlConnection = null;
        private static SqlCommand sqlCommand = null;
        private static DataAccess dataAccess = new DataAccess();

        public static List<UsersRegistration> GetAllUsers()
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Select * from tblUsers";

                List<UsersRegistration> userList = null;
                sqlConnection.Close();
                sqlCommand.Connection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    userList = new List<UsersRegistration>();

                    while (dataReader.Read())
                    {
                        UsersRegistration user = new UsersRegistration();
                        user.UserId = Convert.ToInt32(dataReader["UserId"]);
                        user.UserName = dataReader["UserName"].ToString();
                        userList.Add(user);
                    }
                }
                dataReader.Close();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return userList;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }
        public static List<UsersRegistration> GetUsersList()
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Select * from tblUsers";

                List<UsersRegistration> userList = null;
                sqlConnection.Close();
                sqlCommand.Connection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    userList = new List<UsersRegistration>();

                    while (dataReader.Read())
                    {
                        UsersRegistration user = new UsersRegistration();
                        user.UserId = Convert.ToInt32(dataReader["UserId"]);
                        user.UserName = dataReader["UserName"].ToString();
                        user.UserEmail = dataReader["Email"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        userList.Add(user);
                    }
                }
                dataReader.Close();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return userList;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }
        public static Int32 SaveUser(UsersRegistration user)
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "sp_UserReg";

                sqlConnection.Close();
                sqlCommand.Connection.Open();

                sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
                sqlCommand.Parameters.AddWithValue("@UserName", user.UserName);
                sqlCommand.Parameters.AddWithValue("@Email", user.UserEmail);
                sqlCommand.Parameters.AddWithValue("@Password", user.Password);

                int result = sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return result;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }
    }
}