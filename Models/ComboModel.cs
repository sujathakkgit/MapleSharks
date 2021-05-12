using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;

namespace MapleSharks.Models
{
    public class ComboModel
    {
        //swimmermodel
        [DisplayName("ID : ")]
        public int Id { get; set; }
        [DisplayName("Parent ID : ")]
        public int parentid { get; set; }
        [DisplayName("Name of the swimmer : ")]
        public string swimmername { get; set; }

        [DisplayName("Swim group : ")]
        public string swimgroup { get; set; }


        //[Range((DateTime.Today-100), DateTime.Today)]
        [DisplayName("Date of Birth: ")]
        public DateTime dob { get; set; }
        [DisplayName("Account active(Y/N):")]
        public char active { get; set; }
        public int swrTyp { get; set; }

        //poolusermodel
        //public int Id { get; set; }
        [DisplayName("First name: ")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        //public char active { get; set; }
        public int usrTyp { get; set; }

        //accountdetails
        [DisplayName("First name: ")]
        public string firstName { get; set; }
        [DisplayName("Last name: ")]
        public string lastName { get; set; }
        [DisplayName("Picture: ")]
        public dynamic cPicture { get; set; }

        public AccessType cUserAccessType { get; set; }
        [DisplayName("Address(no,street,city,zipcode): ")]

        public int cUsrAddressNo { get; set; }
        public string cUsrAddressStreet { get; set; }
        public string cUsrAddressCity { get; set; }
        public int cUsrAddressZipcode { get; set; }

        public string ImagePath { get; set; }
        public IFormFile ImageData { get; set; }
        public enum AccessType
        {
            Admin, User
        }
        //public struct cAddress
        //{
        //    public int No;
        //    public string Street;
        //    public string City;
        //    public int Zipcode;

        //    public override string ToString()
        //    {
        //        return "" + No + " " + Street + " " + City + " " + Zipcode;
        //    }

       // }

    }
}
