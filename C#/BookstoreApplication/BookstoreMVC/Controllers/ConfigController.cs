using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNet.SignalR.Hubs;
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
        [HttpGet]
        public async Task<IActionResult> Index()
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
            return View(items);
        }


        [HttpGet]
        public IActionResult Edit()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized", "Shared");
            }
            return View();
        }




        [HttpGet]
        [Route("Config/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("token");
            TempData["view"] = "update";
            if (token == null)
            {
                return View("Unauthorized", "Shared");
            }
            var item = await _service.GetConfigById(id, token);
            if (item == null)
            {
                TempData["failure"] = "Error occurred";
            }
            return View(item);
        }

        [HttpPost]
        [Route("Config/Edit/{Id:int}")]
        public async Task<IActionResult> Edit(int Id, Config config)
        {
            TempData["success"] = "Updated successfully";
            var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
            await _service.UpdateConfig(Id, config, token);
            return RedirectToAction("Index", "Config");
        }

    }
}
