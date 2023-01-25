using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace eBuisness.ViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(100)]
        public string? Fullname { get; set; }
        [Required, MaxLength(100)]
        public string? Username { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required,Compare(nameof(Password)), DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
