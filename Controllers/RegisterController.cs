using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapleSharks.Services;
using MapleSharks.Models;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MapleSharks.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string usrIdcookie;
        private int usrSessionId;

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult ShowRegistrationForm()
        //{
        //    ViewBag.Message = "I am in RegisterSwimmerControl";
        //    ViewBag.swrTyp = 0; //newSwimmer
        //                        // usrSessionId = ViewBag.usrSessionID;
        //    return View("RegisterSwimmer");
        //}
        public RegisterController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this.usrIdcookie = _httpContextAccessor.HttpContext.Request.Cookies["usrIdcookie"];
            this.usrSessionId = Convert.ToInt32(usrIdcookie);

           

        }
        public IActionResult AccountData(ComboModel comModel)
        {
            ViewBag.Message = "I am in RegisterControl:AccountData";
            DBservice secSer = new DBservice();
            
            comModel = secSer.FetchAccount(comModel,usrSessionId);

            return View("AccountData", comModel);
        }
        public IActionResult UpdateAccount(ComboModel comModel)
        {
            ViewBag.Message = "I am in RegisterControl:UpdateAccount";
            DBservice secSer = new DBservice();
            ComboModel tmpModel= comModel;
            comModel = secSer.FetchAccount(comModel, usrSessionId);
            comModel.firstName = tmpModel.firstName;
            comModel.lastName = tmpModel.lastName;
            comModel.cUsrAddressNo = tmpModel.cUsrAddressNo;
            comModel.cUsrAddressStreet = tmpModel.cUsrAddressStreet;
            comModel.cUsrAddressCity = tmpModel.cUsrAddressStreet;
            comModel.cUsrAddressZipcode = tmpModel.cUsrAddressZipcode;

            comModel.cPicture = tmpModel.cPicture;
            comModel.ImageData = (tmpModel.ImageData == null ? comModel.ImageData: tmpModel.ImageData) ;
            secSer.UpdateAccount(comModel);

            return View("AccountDetails", comModel);
        }
          
         public IActionResult AccountDetails(ComboModel comModel)
        {
            ViewBag.Message = "I am in RegisterControl:AccountDetails";
            DBservice secSer = new DBservice();
            ViewBag.Message = "I am in RegistrationController: Account Details";
            comModel = secSer.FetchAccount(comModel, usrSessionId);
            return View("AccountDetails", comModel);
        }
        public IActionResult ProcessRegistration(UserModel usrmodel)
        {
            DBservice secSer = new DBservice();
            ViewBag.Message = "I am in RegistrationController: Process Registration ";
            
            secSer.PoolUserExists(usrmodel,"register");
            ViewBag.usrTyp = usrmodel.usrTyp;
            return View("RegisterStatus", usrmodel);
           
        }//processRegistration
       
        //public async Task<IActionResult> AddPicture(string imgpath, IFormFile formFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var filePath = Path.GetTempFileName();
        //        if (formFile.Length > 0)
        //        {
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
           
        //        //db.Image.Add(img);
        //        //db.SaveChanges();
        //        return  View(filePath);
        //    }
        //    //return View(img);
        //    //return View();
           
        //}

    }
}
