using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team2.Models;

namespace Team2.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        QUANLYCANTEENEntities model = new QUANLYCANTEENEntities();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            Session["password-incorrect"] = false;
            Session["user-not-found"] = false;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string pass)
        {
            Session["password-incorrect"] = false;
            Session["user-not-found"] = false;
            Session["user-fullname"] = false;
            Session["user-id"] = false;
            Session["user-role"] = false;
            var user = model.ACCOUNTs.FirstOrDefault(s => s.EMAIL.Equals(email));
            if (user != null)
            {
                if (user.PASSWORD.Equals(pass))
                {
                    Session["user-fullname"] = user.FULL_NAME;
                    Session["user-id"] = user.ID;
                    Session["user-role"] = user.ROLE;
                    return RedirectToAction("Index", "AdminHome");
                }
                else
                {
                    Session["password-incorrect"] = true;
                    return View();
                }
            }
            Session["user-not-found"] = true;
            return View();
        }
        public ActionResult Logout()
        {
            Session["user-fullname"] = null;
            Session["user-id"] = null;
            return RedirectToAction("Login");
        }
    }
}