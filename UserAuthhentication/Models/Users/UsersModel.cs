using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace UserAuthhentication.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Enter email address")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class UsersRegistration
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = "Enter user name")]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter email address")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        [Display(Name = "User Name (email)")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Enter user password")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuCode { get; set; }
        public string ParentId { get; set; }
        public string MenuText { get; set; }
        public string MenuUrl { get; set; }
        public int MenuOrder { get; set; }
        public List<UserMenu> UserMenuList { get; set; }
        public bool IsActive { get; set; }

        public string GetMenuPermissionXML()
        {
            StringBuilder xmlStore = new StringBuilder();
            using (XmlWriter writer = new XmlTextWriter(new StringWriter(xmlStore)))
            {
                writer.WriteStartElement("permission");
                foreach (UserMenu i in UserMenuList)
                {
                    i.WriteXml(writer);
                }
                writer.WriteEndElement();
            }
            return xmlStore.ToString();
        }        
    }
    public class UserMenu
    {
        public int UserId { get; set; }
        public int UserMenuId { get; set; }
        public bool Permission { get; set; }

        public void WriteXml(XmlWriter writer)
        {
            Menu menu = new Menu();
            writer.WriteStartElement("menu");
            writer.WriteAttributeString("UserId", this.UserId.ToString());
            writer.WriteAttributeString("MenuId", this.UserMenuId.ToString());
            writer.WriteAttributeString("UserPermission", this.Permission.ToString());
            writer.WriteEndElement();
        }
    }
}