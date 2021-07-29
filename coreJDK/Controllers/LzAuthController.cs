using Microsoft.AspNetCore.Mvc;

namespace coreJDK.Controllers
{
    public class LzAuthController : Controller
    {
        public IActionResult Index(string tenantId)
        {
            return new RedirectResult("/User/Index?tenantId=" + tenantId);
        }
    }
}