using AdoRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDOListModel;

namespace ToDoWebsite.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        ToDoListDBEntities1 db = new ToDoListDBEntities1();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users user)
        {
            using(var context = db)
            {
                bool isValid = context.Users.Any(x => x.UserName == user.UserName && x.Password == user.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("GetAllTasks", "Home");
                }
                ModelState.AddModelError("", "Invalid username and password");
            }
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Users user)
        {
            if (ModelState.IsValid)
            {
                using (var context = db)
                {
                    context.Users.Add(new User
                    {
                        UserName = user.UserName,
                        Password = user.Password
                    });
                    context.SaveChanges();
                }
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}