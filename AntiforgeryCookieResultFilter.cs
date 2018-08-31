using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ParityService
{
  internal sealed class AntiforgeryCookieResultFilter : ResultFilterAttribute
  {
    private IAntiforgery m_antiforgery;
    public AntiforgeryCookieResultFilter(IAntiforgery antiforgery)
    {
      m_antiforgery = antiforgery;
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
      var tokens = m_antiforgery.GetAndStoreTokens(context.HttpContext);
      context.HttpContext.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions() { HttpOnly = false });
    }
  }
}
