using System;
namespace ParityUI.Models
{
    public sealed class AppUserViewModel
    {
        public string Email { get; } 

        public string Id { get; }

        public bool EmailConfirmed { get; }

        public AppUserViewModel(AppUser user) {
            Email = user.Email;
            Id = user.Id;
            EmailConfirmed = user.EmailConfirmed;
        }
    }
}
