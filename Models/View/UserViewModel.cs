using System.ComponentModel.DataAnnotations;

namespace ParityService.Models.View
{
  public sealed class UserViewModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; }

    [Required]
    public string Id { get; }

    [Required]
    public bool EmailConfirmed { get; }

    public UserViewModel(User user)
    {
      Email = user.Email;
      Id = user.Id;
      EmailConfirmed = user.EmailConfirmed;
    }
  }
}
