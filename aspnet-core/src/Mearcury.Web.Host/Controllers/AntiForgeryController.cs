using Microsoft.AspNetCore.Antiforgery;
using Mearcury.Controllers;

namespace Mearcury.Web.Host.Controllers
{
    public class AntiForgeryController : MearcuryControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
