using System;
using System.ComponentModel.DataAnnotations;

namespace ParityService.Models.View
{
    public sealed class AppUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; } 

        [Required]
        public string Id { get; }

        [Required]
        public bool EmailConfirmed { get; }

        public AppUserViewModel(AppUser user) {
            Email = user.Email;
            Id = user.Id;
            EmailConfirmed = user.EmailConfirmed;
        }
    }
}
