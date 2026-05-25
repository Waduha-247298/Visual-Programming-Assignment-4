using System.ComponentModel.DataAnnotations;

namespace App02.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "First name is a required field.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is a required field.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address cannot be left blank.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email format (e.g., user@example.com).")]
        public string Email { get; set; } = string.Empty;
    }
}