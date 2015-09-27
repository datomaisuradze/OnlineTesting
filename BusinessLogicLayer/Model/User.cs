using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Model
{
    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter you name")]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter you E-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter you Password")]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password minimum length is 6 characters, maximum-50")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Please repeat your password")]
        [Display(Name = "Repeate Password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Repeated password minimum length is 6 characters, maximum-50")]
        public string UserRepeatPassword { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public int Points { get; set; }
    }
}
