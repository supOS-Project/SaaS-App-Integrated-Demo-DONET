using coreJDK.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace coreJDK.Controllers
{
    public class UserController : Controller
    {

        private IUserService _iUserService { get; set; }

        public UserController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }

        public IActionResult Index(string tenantId)
        {
            var userList = this._iUserService.GetUserListByTenantId(tenantId);
            return Content(JsonConvert.SerializeObject(userList));
        }
    }
}