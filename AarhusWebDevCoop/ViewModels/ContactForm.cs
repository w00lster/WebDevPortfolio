using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
    public class ContactForm {

        [Required(ErrorMessage = "Please enter your name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your Email address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage ="Please enter a valid Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a Subject!")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter a Message!")]
        public string Message { get; set; }
    }
}