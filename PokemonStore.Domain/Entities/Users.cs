using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PokemonStore.Domain.Entities
{
   public class Users
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your login")]
        public string Login { get; set; }
        public string CryptedPassword { get; set; }
        public string PasswordSalt { get; set; }
        [Required(ErrorMessage = "Please enter your phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
    }
}
