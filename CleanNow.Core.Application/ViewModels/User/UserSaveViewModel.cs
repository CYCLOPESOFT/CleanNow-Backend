using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.ViewModels.User
{
    public class UserSaveViewModel
    {
        [Required(ErrorMessage ="Ingresar el nombre")]
        [DataType(DataType.Text)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Ingresar el numero de telefono")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Ingresar el nombre de usuario")]
        [DataType(DataType.Text)]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "Ingresar el email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Ingresar el email")]
        [DataType(DataType.Text)]
        public string? Code { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
