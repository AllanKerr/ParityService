using System;
using System.ComponentModel.DataAnnotations;

namespace ParityService.Models.View
{
  public sealed class ServiceLinkViewModel
  {
    [Required]
    public int Id { get; private set; }

    [Required]
    public bool IsPractice { get; private set; }

    [Required]
    public DateTime CreatedAt { get; private set; }

    [Required]
    public bool IsAuthenticated { get; private set; }

    public ServiceLinkViewModel(ServiceLink link)
    {
      Id = link.Id;
      IsPractice = link.IsPractice;
      CreatedAt = link.CreatedAt;
      IsAuthenticated = link.Credentials != null;
    }
  }
}