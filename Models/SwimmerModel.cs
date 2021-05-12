using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MapleSharks.Models
{
    
    public class SwimmerModel
    {
        //SUJ - add properties
        //public int usrSessionid { get; set; }

        [DisplayName("ID : ")] 
        public int Id { get; set; }
        [DisplayName("Parent ID : ")]
        public int parentid { get; set; }
        [DisplayName("Firstname : ")]
        
        public string swrFname { get; set; }
        [DisplayName("Lastname : ")]
        public string swrLname { get; set; }
       
        [DisplayName("Swim group : ")]
        public string swimgroup { get; set; }
      
        
        //[Range((DateTime.Today-100), DateTime.Today)]
        [DisplayName("Date of Birth: ")]
        public DateTime dob { get; set; }
        [DisplayName("Account active(Y/N):")]
        public char active { get; set; }
        public int swrTyp { get; set; }
    }

    

    
}
