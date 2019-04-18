using System.ComponentModel.DataAnnotations;

namespace ProjectFinal101.Core.Resources
{
    public class RegisterResource
    {
        public string UserName { get; set; }


        public string Password { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "MIN_PASSWORD_LENGTH")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
