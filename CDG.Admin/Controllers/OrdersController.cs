using AutoMapper;
using CDG.Admin.Extensions;
using CDG.Admin.Interfaces;
using CDG.Admin.Models;
using CDG.Admin.Models.Order;
using CDG.Admin.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CDG.Admin.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService orderService;
    private readonly IMapper mapper;

    public OrdersController(IOrderService orderService,
    IMapper mapper)
    {
        this.orderService = orderService;
        this.mapper = mapper;
    }

    //Display index view with all buyers and their number of unproc orders
    public async Task<IActionResult> Index()
    {
        var vm = new OrdersPageViewModel();

        var response = await orderService.GetAllBuyersAsync<ResponseDTO>();
        if(response != null && response.IsSuccess)
        {
            var list = JsonConvert.DeserializeObject<List<BuyerDTO>>(Convert.ToString(response.Result)!);
            vm.Buyers = list!.Select(d => mapper.Map<BuyerViewModel>(d)).OrderByDescending(x => x.UnproccessedOrdersCount).ToList();
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            vm.IsSuccess = false;
            return View(vm);
        }

        if(vm.Buyers.Count == 0) vm.StatusMessage = "Nothing found";

        return View(vm);
    }
    //get list of orders by buyerId
    [HttpGet]
    public async Task<IActionResult> GetBuyersOrders(string buyerId, string buyerName, int count)
    {
        var response = await orderService.GetUserWithOrdersByIdAsync<ResponseDTO>(buyerId);
        var vm = new OrdersPageViewModel();

        if(response != null && response.IsSuccess)
        {
            var stringRes = Convert.ToString(response.Result)!;
            var ordersDTOs = JsonConvert.DeserializeObject<List<OrderDTO>>(stringRes);
            var orders = ordersDTOs!.Select(o => mapper.Map<OrderViewModel>(o)).ToList();

            vm.ProccessedOrders.AddRange(orders.Where(o => o.IsInProcess == false));
            vm.UnproccessedOrders.AddRange(orders.Where(o => o.IsInProcess == true));

            vm.BuyerId = buyerId;
            vm.BuyerName = buyerName;
            vm.UnproccessedOrdersCount = count;
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            vm.IsSuccess = false;

        }
        return PartialView("_OrdersMenuPartial", vm);
    }
    [HttpGet]
    public async Task<IActionResult> GetOrderDetails(int id)
    {
        var response = await orderService.GetOrderDetails<ResponseDTO>(id);
        var order = new OrderViewModel();
        if(response != null && response.IsSuccess)
        {
            var orderDTO = JsonConvert.DeserializeObject<OrderDTO>(Convert.ToString(response.Result)!);
            order = mapper.Map<OrderViewModel>(orderDTO);

            switch (order.DeliveryType)
            {
                case "FreeShipment":
                    order.DeliveryType = "Стандартная доставка";
                break;
                case "Self_delivery":
                    order.DeliveryType = "Самовывоз";
                break;
                case "PostShipment":
                    order.DeliveryType = "Почтой";
                break;
            }
            switch (order.PaymentType)
            {
                case "Cash":
                    order.PaymentType = "Наличные";
                break;
                case "PaymentCard":
                    order.PaymentType = "Карта";
                break;
            }

            order.FullPrice = order.OrderItems.Sum(i => i.TotalPrice);

            if(Request.IsAjaxRequest())
                return PartialView("OrderDetails",order);
            else
                return View("OrderDetails",order);
        }
        else
        {
            order.IsSuccess = false;
            order.StatusMessage = "Error loading data. Try later.";
            return View(order);
        }
    }

    //too many requests
    [HttpPost]
    public async Task<IActionResult> ApproveOrder(int orderId)
    {
        var responseApprove = await orderService.ApproveOrder<ResponseDTO>(orderId);
        var vm = new OrdersPageViewModel();
        if(responseApprove != null && responseApprove.IsSuccess)
        {
            var response = await orderService.GetOrderDetails<ResponseDTO>(orderId);
            var order = new OrderViewModel();
                        var orderDTO = JsonConvert.DeserializeObject<OrderDTO>(Convert.ToString(response.Result)!);
            order = mapper.Map<OrderViewModel>(orderDTO);

            switch (order.DeliveryType)
            {
                case "FreeShipment":
                    order.DeliveryType = "Стандартная доставка";
                break;
                case "Self_delivery":
                    order.DeliveryType = "Самовывоз";
                break;
                case "PostShipment":
                    order.DeliveryType = "Почтой";
                break;
            }
            switch (order.PaymentType)
            {
                case "Cash":
                    order.PaymentType = "Наличные";
                break;
                case "PaymentCard":
                    order.PaymentType = "Карта";
                break;
            }
            if(Request.IsAjaxRequest())
                return PartialView("OrderDetails",order);
            else
                return View("OrderDetails",order);
        }
        else
        {
            vm.IsSuccess = false;
            vm.StatusMessage = "Error loading data. Try later.";
            return View("Index", vm);
        }
    }
}