using System.ComponentModel.DataAnnotations;
using ParityService.Models.Entities;

namespace ParityService.Models.View
{
  public sealed class QuestradeLinkViewModel : IQuestradeLink
  {
    [Required]
    public string RefreshToken { get; set; }

    public bool IsPractice { get; set; }
  }
}