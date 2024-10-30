using BookstoreMVC.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreMVC.Controllers
{
    public class ConfigController : Controller
    {
        private readonly IConfigService _service;
        public ConfigController(IConfigService service)
        {
            _service = service;
        }
        [HttpGet, ActionName("GetAllConfigValues")]
        public async Task<IActionResult> GetAllConfigValues()
        {
            var token = HttpContext.Session.GetString("token");
            var role = HttpContext.Session.GetString("roleId");
            TempData["role"] = role;
            if (token == null)
            {
                return View("Unauthorized");
            }
            var items = await _service.GetAllConfigValues(token);
            var res= Json(new { Data = items });
            return res;
        }
    }
}
