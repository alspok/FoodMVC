using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodMVC.Models
{
    public class SendMailModel
    {
        public string EmailUser { get; set; } = "38374d4eabe748b9f1de7552f008cdf2";

        public string Password { get; set; } = "6bc3ec2bfd6ca6b25a1778b197d45c5c";

        [Display(Name = "Nuo")]
        public string EmailFrom { get; set; } = "a.spokauskas@outlook.com";

        [Required(ErrorMessage = "Reikia įvesti el. pašto adresą")]
        [Display(Name = "Kam")]
        [DataType(DataType.EmailAddress)]
        public string EmailTo { get; set; }

        [Display(Name = "Tema")]
        public string Subject { get; set; } = "Prekių sąrašas";
        

        [Display(Name ="Laiškas")]
        public string EmailText { get; set; }
        
        public string UserRole { get; set; }
    }
}