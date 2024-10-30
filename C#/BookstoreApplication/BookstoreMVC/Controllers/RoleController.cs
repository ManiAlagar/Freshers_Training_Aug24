using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookstoreMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _service.GetAllRoles();
            var res = roles.Select(u => new SelectListItem
            {
                Value = u.RoleId.ToString(),
                Text=u.RoleName
            });
            ViewBag.role = res;
            return RedirectToAction("Register", "User");
        }  

    }
}

