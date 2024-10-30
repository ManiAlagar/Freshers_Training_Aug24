using BookstoreMVC.Services.Interface;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading;

namespace BookstoreMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly IStatusService service;
        public OrderController(IOrderService service, IStatusService __service)
        {
            this._service = service;
            this.service = __service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var Statuses = await service.GetAllOrderStatus();
                var res = Statuses.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.status
                });
                ViewBag.statuses = res;
                var token = HttpContext.Session.GetString("token");
                var role = HttpContext.Session.GetString("roleId");
                TempData["role"] = role;
                
                if (token == null|| role=="2")
                {
                    return View("Unauthorized", "Shared");
                }
                if (role == "1")
                {
                    
                    var customerOrder = await _service.GetAllOrders(token);
                    return View(customerOrder);
                }
                else
                {
                    var orders = await _service.GetAllOrders(token);
                    return View(orders);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost, ActionName("AddOrder")]

        public async Task<int> AddOrder([FromQuery] string Address)
        {

            try
            {
                TempData["success"] = "Order placed successfully";
                var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
                await _service.AddOrder(Address, token);
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpPost, ActionName("DeleteOrder")]
        public async Task<bool> DeleteOrder([FromQuery] string orderId)
        {

            try
            {
                TempData["success"] = "Order cancelled";
                var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
                await _service.DeleteOrder(Convert.ToInt32(orderId), token);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet, ActionName("GetBooksFromOrder")]
        public async Task<IActionResult> GetBooksFromOrder([FromRoute] int id)
        {
            var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
            var books=await _service.GetBooksFromOrder(id,token);
            return View(books);

        }




        [HttpPost, ActionName("UpdateOrder")]
        public async Task<bool> UpdateOrder(int orderId,int statusId)
        {

            var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
            await _service.UpdateOrder(orderId, statusId, token);
            return true;
        }

        //[HttpGet, ActionName("GetOrderById")]
        //public async Task<bool> GetOrderById(int id)
        //{

        //    var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
        //    await _service.GetOrderById(id, token);
        //    return true;
        //}


    }
}
// /api/Order/GetAllOrders /api/Order/AddOrder /api/Order/GetAllOrders /api/Order/GetOrderById/ /api/Order/DeleteOrder /api/Order/UpdateOrder
// /api/Order/GetBooksFromOrder/
//@model IEnumerable<BookstoreMVC.Models.Order>
//<div>
//    <h6>My ordered books</h6>
//</div>

//<table class= "table" >
//    < thead >
//        < tr >
//            < th > Title </ th >
//            < th > Quantity </ th >
//        </ tr >
//    </ thead >
//     < tbody >
//         @foreach(var item in Model)
//         {
//             < tr >
//                 < td > @item.Title </ td >
//                 < td > @item.Quantity </ td >
//             </ tr >
//         }
//     </ tbody >
//</ table >
