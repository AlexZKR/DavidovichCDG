using CDG.BLL;
using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Order;
using CDG.BLL.Interfaces;
using CDG.DAL.Data;
using CDG.Web.Interfaces;
using CDG.Web.Models.Order;
using CDG.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CDG.Web.Controllers;
[Authorize]
[Route("Order")]
public class OrderController : Controller
{
    private const string ORDER_VM_BINDING_ATTRIBUTES = "OrderItems, OrderDate, TotalPrice, FullPrice, DiscountSize, TotalItems, DeliveryType, IsInProccess, OrderComment, BuyerId, FirstName, LastName, PhoneNumber, Email, Street, City, PostCode, Region, PaymentType, DeliveryType";
    private readonly IOrderService orderService;
    private readonly ICheckOutViewModelService checkOutViewModelService;
    private readonly IBasketService basketService;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IAppLogger<OrderController> logger;
    private readonly IBasketViewModelService basketViewModelService;
    private readonly IOrderViewModelService orderViewModelService;
    private string? username = null;

    public OrderController(IOrderService orderService,
    ICheckOutViewModelService checkOutViewModelService,
    IBasketService basketService,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IAppLogger<OrderController> logger,
    IBasketViewModelService basketViewModelService,
    IOrderViewModelService orderViewModelService)
    {
        this.orderService = orderService;
        this.checkOutViewModelService = checkOutViewModelService;
        this.basketService = basketService;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.logger = logger;
        this.basketViewModelService = basketViewModelService;
        this.orderViewModelService = orderViewModelService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var checkVM = await SetCheckOutVMAsync();
            if (user != null)
            {
                checkVM.FirstName = user.FirstName;
                checkVM.LastName = user.LastName;
                checkVM.Email = user.Email;
                checkVM.PhoneNumber = user.PhoneNumber;
            }
            return View(checkVM);
        }
        catch (ArgumentNullException)
        {
            return RedirectToAction("Index", "Catalog");
        }
        catch(Exception)
        {
            throw;
        }

    }

    [Route("PlaceOrder")]
    [HttpPost("PlaceOrder")]
    public async Task<IActionResult> PlaceOrder([Bind(ORDER_VM_BINDING_ATTRIBUTES)] CheckOutViewModel vm)
    {
        GetOrSetBasketCookieAndUserName();
        MapCheckoutVm(vm, out Buyer buyer, out OrderInfo orderInfo);
        Order order = await orderService.CreateOrderAsync(buyer, orderInfo);
        var orderVm = await orderViewModelService.CreateOrderViewModelAsync(order.Id);

        

        return View("OrderDetails",orderVm);
    }

    [Route("[action]/{orderId:int}")]
    public async Task<IActionResult> OrderDetails(int orderId)
    {
        var orderVm = await orderViewModelService.CreateOrderViewModelAsync(orderId);
        return View(orderVm);
    }

    //private helpers
    private async Task<CheckOutViewModel> SetCheckOutVMAsync()
    {
        var vm = new CheckOutViewModel();

        if (signInManager.IsSignedIn(HttpContext.User))
        {
            vm = await checkOutViewModelService.CreateCheckOutVMAsync(User.Identity!.Name!);
        }
        else
        {
            GetOrSetBasketCookieAndUserName();
            vm = await checkOutViewModelService.CreateCheckOutVMAsync(username!);
        }
        return vm;
    }

    private void GetOrSetBasketCookieAndUserName()
    {
        if (Request.Cookies.ContainsKey(SD.BASKET_COOKIENAME))
        {
            username = Request.Cookies[SD.BASKET_COOKIENAME];
        }
        if (username != null) return;

        username = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.Today.AddYears(10);
        Response.Cookies.Append(SD.BASKET_COOKIENAME, username, cookieOptions);
    }

    private void MapCheckoutVm(CheckOutViewModel vm, out Buyer buyer, out OrderInfo orderInfo)
    {
        buyer = new Buyer
        {
            BuyerId = vm.BuyerId,
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            PhoneNumber = vm.PhoneNumber,
            Email = vm.Email
        };

        orderInfo = new OrderInfo
        {
        };
    }
}