using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using MapleSharks.Models;
using MapleSharks.Services;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;

namespace MapleSharks.Controllers
{
    public class RegisterSwimmerController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private string usrIdcookie;
        private int usrSessionId;



        public RegisterSwimmerController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this.usrIdcookie = _httpContextAccessor.HttpContext.Request.Cookies["usrIdcookie"];
            this.usrSessionId = Convert.ToInt32(usrIdcookie);

        }
        //HttpCookie usrIdcookie;
        public IActionResult Index()
        {
            ViewBag.swrTyp = 0;
            return View();
        }

        public IActionResult ShowSwimmerDataCard(SwimmerModel swimmodel)
        {
            ViewBag.Message = "I am in RegisterSwimmerController:ShowSwimmerDatacard";
            int usrSessionId;
            DBservice secSer = new DBservice();
            String usrIdcookie = _httpContextAccessor.HttpContext.Request.Cookies["usrIdcookie"];
            usrSessionId = Convert.ToInt32(usrIdcookie);
            List<SwimmerModel> swrList = new List<SwimmerModel>();
            swrList = secSer.getSwimmerDetails(swimmodel, usrSessionId).ToList();
            ViewBag.usrTyp = 3;
            return View("CardView", swrList);
        }
        public IActionResult ShowSwimmerData(SwimmerModel swimmodel)
        {
            ViewBag.Message = "I am in RegisterSwimmerController:showSwimmerData";

            int usrSessionId;
            DBservice secSer = new DBservice();
            String usrIdcookie = _httpContextAccessor.HttpContext.Request.Cookies["usrIdcookie"];
            usrSessionId = Convert.ToInt32(usrIdcookie);
            List<SwimmerModel> swrList = new List<SwimmerModel>();
            swrList = secSer.getSwimmerDetails(swimmodel,usrSessionId).ToList();
            ViewBag.usrTyp = 3;
            return View("UserSwimmerData", swrList);
            //ViewBag.SwrExists = secSer.swimmerExists(swimmodel, usrSessionId);
            //if (ViewBag.swrExists)
            //{
            //    List<SwimmerModel> swrList = new List<SwimmerModel>();
            //    swrList.Add(swimmodel); 
            //    return View("UserSwimmerData", swrList);
            //}
            //else
            //{ return View("Index"); }
        }//showswimmerdata
      
        public IActionResult ShowRegistrationForm()
        {
            ViewBag.Message = "I am in RegisterSwimmerControl:ShowRegistrationForm";
            ViewBag.swrTyp = 0; //newSwimmer
           // usrSessionId = ViewBag.usrSessionID;
            return View("RegisterSwimmer");
        }

        //sujatha add cookie
        //public IActionResult Edit()
        //{
        //    return View();
        //}
      

        public IActionResult ProcessSwimmerReg(SwimmerModel swimmodel)
        {
            ViewBag.Message = "I am in RegisterSwimmerController:ShowSwimmerDatacard";

            DBservice secSer = new DBservice();
            
          // ViewBag.Message = "I am in RegisterSwimmerControl : ProcessSwimmerReg";
            

            //String usrIdcookie = _httpContextAccessor.HttpContext.Request.Cookies["usrIdcookie"];
            //usrSessionId = Convert.ToInt32(usrIdcookie);
            swimmodel.parentid = usrSessionId;
            
            if (secSer.swimmerExists(swimmodel, usrSessionId))
            {
              //  ViewBag.swrTyp = 2;
                List<SwimmerModel> tempList = new List<SwimmerModel>();
                tempList.Add(swimmodel);
                ViewBag.swrTyp = swimmodel.swrTyp;
                if (swimmodel.swrTyp == 2)
                {
                    return View("UserSwimmerData", tempList);
                }
                return View("RegisterSwimmer", swimmodel);
            }
            else
            {
                if (secSer.addNewSwimmer(swimmodel, usrSessionId))
                {
                    ViewBag.swrTyp = swimmodel.swrTyp;
                }
                return View("RegisterSwimmer",swimmodel); // goes here for newly added 
            }
                //List<SwimmerModel> swrList = new List<SwimmerModel>();
            //swrList.Add(swimmodel);
            //return View("UserSwimmerData", swrList);
            //return View("UserSwimmerData", swimmodel);

        }//ProcessSwimmerReg
        
       
    }

    
}
