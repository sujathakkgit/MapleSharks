using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Suj add model
using MapleSharks.Models;
using MapleSharks.Services;
using Microsoft.AspNetCore.Http;
namespace MapleSharks.Controllers
{
    [LogActionFilter]
    public class LoginController : Controller
    {
        CookieOptions usrIdcookopt = new CookieOptions();

        //sujatha add cookie
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        //SUJ - add ProcessLogin

        public IActionResult ProcessLogin(UserModel usrmodel)
        {
            DBservice secSer = new DBservice();
            secSer.PoolUserExists(usrmodel, "login");
            ViewBag.usrTyp = usrmodel.usrTyp;
            ViewBag.usrSessionID = usrmodel.Id;
            ViewBag.Message = "I am in LoginController:ProcessLogin";
            
            if (ViewBag.usrTyp == 1)
            {
                //cookie creation
                usrIdcookopt.Expires = DateTime.Now.AddDays(1);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("usrIdcookie", usrmodel.Id.ToString(), usrIdcookopt);
                // ControllerContext.HttpContext.Session.Set("parentid",usrmodel);
                return View("LoginStatus", usrmodel);
            }

            return View("LoginStatus", usrmodel);
        }//Processlogin

        public IActionResult ProcessLogout(UserModel usrmodel)
        {
            DBservice secSer = new DBservice();
            ViewBag.usrTyp = 2; //to denote its logged out
           ViewBag.Message = "I am in LoginController:Logout";

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("usrIdcookie");

            return View("LoginStatus", usrmodel);
        }//Processlogout

    }//controller
}//namespace