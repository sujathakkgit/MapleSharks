using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;

namespace MapleSharks.Models
{
    public class ComboModel_try
    {
        public UserModel poolUsrModel { get; set; }
        public SwimmerModel swrModel { get; set; }
        
    }
}
