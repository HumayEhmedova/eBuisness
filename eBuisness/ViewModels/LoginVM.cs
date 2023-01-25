using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace eBuisness.ViewModels
{
    public class LoginVM
    {
        [Required,MaxLength(100)]
        public string? Username { get; set; }

        [Required, DataType(DataType.Password)] 
        public string? Password { get; set; }
        public bool RemembeMe { get; set; }
    }
}
