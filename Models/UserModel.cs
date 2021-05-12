using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapleSharks.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public int usrSessionid => (Id);
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public char active { get; set; }
        public dynamic CPicture{get; set;}

        public int cUsrAddressNo { get; set; }
        public string cUsrAddressStreet { get; set; }
        public string cUsrAddressCity { get; set; }
        public int cUsrAddressZipcode { get; set; }

        public string ImagePath { get; set; }
        public IFormFile ImageData { get; set; }
        public int usrTyp { get; set; }
    }//constructor

}//namespace

