using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

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
            ViewBag.list = roles;
            return (IActionResult)roles;
        }

    }
}
