using CarDealership.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;

namespace CarDealership.UI.Utilities
{
    public class AuthorizeUtilities
    {
        public static string GetUserId(Controller controller)
        {
            var userMgr = new UserManager<ApplicationUser> (
                new UserStore<ApplicationUser> (
                    new ApplicationDbContext()
                )
            );

            var user = userMgr.FindByName(controller.User.Identity.Name);
            return user.Id;
        }
    }
}