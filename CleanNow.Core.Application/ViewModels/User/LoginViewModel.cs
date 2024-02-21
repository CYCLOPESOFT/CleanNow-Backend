using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Debe poner el email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe poner el password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool HasError { get; set; }
        public string Error {  get; set; }
    }
}
