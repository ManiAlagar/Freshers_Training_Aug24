﻿using CRUD_USER.Models;
using Microsoft.AspNetCore.Mvc;
using Users_CRUD.Web.Services.Interfaces;

namespace CRUD_USER.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> Index()
        {

            var user = await service.Get();
            return View(user);
        }

        private async Task<Users> Get(int? id)
        {
            var User = await service.Get(id);
            return User;
        }


        //For Create and Update operation
        [HttpGet]
        public async Task <IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["AddOrEdit"] = "Create";
                return View();
            }
            else
            {
                TempData["AddOrEdit"] = "Edit";
                if (id == null)
                {
                    return NotFound();
                }
                Users user = await Get(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind] Users entity)
        {
           
            TempData["Toastr"] = "Updated Successful";

            if (id != null)
            {
                //Users user = await Get(id);               
                await service.Edit(entity);
            }
            else
            {
                await service.Create(entity);
                TempData["Toastr"] = "Created Successful";
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<bool> DeleteConfirmed(int id)
        {
            service.Delete(id);
            return true;
        }
    }
}
