using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Model
{
    public class ForLogIn
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Please enter you name")]
        [Display(Name = "Name")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please enter you Password")]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password minimum length is 6 characters, maximum-50")]
        public string UserPassword { get; set; }
    }
}
