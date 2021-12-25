using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Form_Authentication_56.Models;
using System.Web.Security;

namespace Form_Authentication_56.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthorizeEntities2 _dbContext = new AuthorizeEntities2();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = _dbContext.Users
               .Any(u => u.UserName.ToLower() == user
               .UserName.ToLower() && user
               .Password == user.Password);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "Employee");
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User registerUser)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Users.Add(registerUser);
                _dbContext.SaveChanges();
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