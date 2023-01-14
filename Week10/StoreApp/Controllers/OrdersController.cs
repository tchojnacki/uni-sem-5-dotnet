using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Services;

namespace StoreApp.Controllers
{
    [Authorize(Roles = Role.Client)]
    public class OrdersController : Controller
    {
        private readonly ICartService _cartService;
        private readonly StoreDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrdersController(
            ICartService cartService,
            StoreDbContext context,
            UserManager<IdentityUser> userManager
        )
        {
            _cartService = cartService;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Submit()
        {
            if (_cartService.GetCart().IsEmpty)
                return RedirectToAction("Index", "Cart");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(Order model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.OwnerName = _userManager.GetUserName(User);
            model.Articles = _cartService
                .GetCart()
                .Items.Select(i => new OrderArticle { ArticleId = i.Article.Id, Count = i.Count })
                .ToList();
            _context.Add(model);
            await _context.SaveChangesAsync();
            _cartService.ClearAll();
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.DeliveryInfo)
                .Include(o => o.Articles)
                .ThenInclude(oa => oa.Article)
                .FirstOrDefault(o => o.Id == id);

            if (order is null)
                return NotFound();

            if (order.OwnerName != _userManager.GetUserName(User))
                return Forbid();

            return View(order);
        }

        public IActionResult Index()
        {
            var model = _context.Orders
                .Include(o => o.DeliveryInfo)
                .Include(o => o.Articles)
                .ThenInclude(oa => oa.Article)
                .Where(o => o.OwnerName == _userManager.GetUserName(User));

            return View(model);
        }
    }
}
