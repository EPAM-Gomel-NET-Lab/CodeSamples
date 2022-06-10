using System.ComponentModel.DataAnnotations;

namespace IdentityService.Contracts.Models
{
    public class UserRegistrationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
