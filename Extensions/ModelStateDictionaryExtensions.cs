using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ParityService.Extensions
{
  public static class ModelStateDictionaryExtensions
  {
    public static void AddErrors(this ModelStateDictionary modelState, IdentityResult result)
    {
      foreach (IdentityError error in result.Errors)
      {
        modelState.AddModelError(string.Empty, error.Description);
      }
    }
  }
}
