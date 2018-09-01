namespace ParityService.Models.Entities
{
  public interface IQuestradeLink
  {
    string RefreshToken { get; }

    bool IsPractice { get; }
  }
}