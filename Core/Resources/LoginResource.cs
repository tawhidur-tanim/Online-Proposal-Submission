using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinal101.Core.Resources
{
    public class LoginResource
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "MIN_PASSWORD_LENGTH")]
        public string Password { get; set; }
    }


    public class RegisterResource
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "MIN_PASSWORD_LENGTH")]
        public string Password { get; set; }
    }

    public class AuthDataResource
    {
        public string Token { get; set; }

        public DateTime ExpireTime { get; set; }

        public string Id { get; set; }
    }
}
