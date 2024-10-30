
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace BookstoreMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _service;
        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpPost, ActionName("AddItemToCart")]
        public async Task<bool> AddItemToCart([FromQuery]string bookId)
        {

            var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
            await _service.AddItemToCart(Convert.ToInt32(bookId), token);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            decimal shipping=0;
            decimal discountoffer=0;
            decimal amount=0;
            var token = HttpContext.Session.GetString("token");
            var role = HttpContext.Session.GetString("roleId");
            TempData["role"] = role;
            if (token == null || role=="3" || role=="2")
            {
                return View("Unauthorized", "Shared");
            }
            var items = await _service.GetAllCartItems(token);
            var discount = await _service.Discount(token);
            foreach(var i in discount)
            {
                shipping = i.Shipping;
                discountoffer = i.DiscountOfffer;
                amount = i.Amount;
            }

            var discountRes = new Discount()
            {
                //DiscountOfffer = discountoffer,
                DiscountOfffer = Convert.ToInt32(discountoffer),
                Shipping = shipping,
                Amount= amount
            };

            var res = new CartRes()
            {
                discount = discountRes,
                items = items
            };
            return View(res);
        }


        [HttpPost, ActionName("DeleteItemFromCart")]
        public async Task<bool> DeleteItemFromCart([FromQuery]string cartItemId)
        {

            var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
            await _service.DeleteItemFromCart(Convert.ToInt32(cartItemId), token);
            return true;
        }
        

        [HttpGet]
        [Route("Cart/GetCartItemById/{id}")]
        public async Task<IActionResult> GetCartItemById(int id)
        {
            var token = HttpContext.Session.GetString("token");
            TempData["view"] = "update";
            if (token == null)
            {
                return View("Unauthorized", "Shared");
            }
            var item = await _service.GetCartItemById(id, token);
            if (item == null)
            {
                TempData["failure"] = "Error occurred";
            }
            return View(item);
        }

        [HttpPost, ActionName("UpdateQuantity")]
        public async Task<bool> UpdateQuantity(int cartItemId,int quantity)
        {
            var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
            await _service.UpdateQuantity(cartItemId, quantity,token);
            return true;
        }


    }
}
