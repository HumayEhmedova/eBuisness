using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace eBuisness.Areas.Admin.ViewModels.Team
{
    public class CreateTeamVM
    {
        [Required,MaxLength(100)]
        public string? Name { get; set; }
        [Required, MaxLength(100)]
        public string? Position { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
    }
}
