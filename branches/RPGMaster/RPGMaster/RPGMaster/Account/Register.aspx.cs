using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using RPGMaster.Models;
using RPGMaster.Data;

namespace RPGMaster.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = new UserManager();
            var user = new ApplicationUser() { UserName = UserName.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                ApplicationUser newUser = manager.Find(UserName.Text, Password.Text);
                var sa = new StoredAccount();
                sa.CreateNewAccount(newUser.Id,Email.Text);
                var returnUrl = Request.QueryString["ReturnUrl"];
                IdentityHelper.SignIn(manager, user, isPersistent: false);
                if (returnUrl == null)
                {
                    IdentityHelper.RedirectToReturnUrl("~/Game/User-Home.aspx", Response);
                }
                else
                {
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}