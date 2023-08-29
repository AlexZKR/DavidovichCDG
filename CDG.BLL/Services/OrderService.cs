using System.ComponentModel.DataAnnotations;
using CDG.BLL.Entities.BasketAggregate;
using CDG.BLL.Entities.Order;
using CDG.BLL.Entities.Products;
using CDG.BLL.Exceptions;
using CDG.BLL.Interfaces;
using CDG.BLL.Specifications.OrderSpecifications;

namespace CDG.BLL.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> orderRepository;
    private readonly IBasketService basketService;
    private readonly IRepository<BaseProduct> productRepository;
    private readonly IAppLogger<OrderService> logger;
    private readonly IRepository<OrderItem> orderItemsRepository;
    private readonly IEmailSender emailSender;
    private static Random random = new Random();


    public OrderService(IRepository<Order> orderRepository,
    IBasketService basketService,
    IRepository<BaseProduct> productRepository,
    IAppLogger<OrderService> logger,
    IRepository<OrderItem> orderItemsRepository,
    IEmailSender emailSender)
    {
        this.orderRepository = orderRepository;
        this.basketService = basketService;
        this.productRepository = productRepository;
        this.logger = logger;
        this.orderItemsRepository = orderItemsRepository;
        this.emailSender = emailSender;
    }
    public async Task<Order> CreateOrderAsync(Buyer buyer, OrderInfo orderInfo)
    {
        if (buyer.BuyerId == null) throw new NotFoundException($"User was not found");
        logger.LogInformation($"Creating order for userId: {buyer.BuyerId}");
        var basket = await basketService.GetBasketAsync(buyer.BuyerId);
        var orderItems = await MapBasketItems(basket.Items);

        //unique key generation
        orderItems.Select(x => { x.Key = RandomString(SD.KEY_SIZE); return x; }).ToList();

        var order = new Order(buyer, orderInfo)
        {
            OrderItems = orderItems,
        };
        await orderRepository.AddAsync(order);

        //email sender (should not be there tbh)
        await emailSender.SendOrderAsync(orderItems, buyer.Email!);

        //deleting basket
        //items in basket sold++

        //This should be done only after the order is proccessed!!!!!
        //IncSoldOnItems(basket.Items);

        await basketService.DeleteBasketAsync(buyer.BuyerId);

        return order;
    }



    public async Task<List<Order>> GetBuyersOrdersAsync(string username)
    {
        if (username == null) throw new NotFoundException($"User was not found");
        var spec = new UserOrdersWithItemsByUsernameSpecification(username);
        var orderList = await orderRepository.ListAsync(spec);
        return orderList;
    }

    //TODO: make this paged, bool proccessed
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var spec = new AllOrdersWithItemsSpecification();
        return await orderRepository.ListAsync(spec);
        // var spec = new OrdersByProccessedSpecification(false);
        // return await orderRepository.ListAsync(spec);
    }

    private async Task<List<OrderItem>> MapBasketItems(IReadOnlyCollection<BasketItem> basketItems)
    {
        var list = new List<OrderItem>();
        foreach (var item in basketItems)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product == null) throw new Exceptions.NotFoundException($"Item with id {item.ProductId} not found in db");

            var orderItem = new OrderItem(item.ProductId,
                                          product.Name,
                                          item.FullPrice,
                                          item.Discount,
                                          item.Quantity);
            list.Add(orderItem);

            // if (item is Book)
            // {
            //     orderItem.AddInfo = $"Автор: {(item as Book)!.Author.Name}";
            // }
        }
        return list;
    }
    private async void IncSoldOnItems(IReadOnlyCollection<BasketItem> basketItems)
    {

        foreach (var item in basketItems)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product != null)
            {
                product.Sold++;
                await productRepository.UpdateAsync(product);
            }
            else
                throw new NotFoundException($"Product with id {item.ProductId} was not fount");
        }
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        var spec = new OrderWithItemsByIdSpecification(id);
        var order = await orderRepository.FirstOrDefaultAsync(spec);
        if (order == null) throw new NotFoundException($"Order with id {id} was not found.");
        return order;
    }

    public async Task<Order> GetOrderByUsernameAsync(string username)
    {
        var spec = new UserOrdersWithItemsByUsernameSpecification(username);
        var order = await orderRepository.FirstOrDefaultAsync(spec);
        if (order == null) throw new NotFoundException($"Order with id {username} was not found.");
        return order;
    }


    public async Task<List<Order>> GetAllBuyersAsync()
    {
        var spec = new BuyersOnlySpecification();
        var buyers = await orderRepository.ListAsync(spec);
        //set non proccessed orders count for api notification
        foreach (var item in buyers)
        {
            if (item.Buyer.BuyerId == null) throw new NotFoundException($"User was not found");
            var countSpec = new UnproccessedByUserNameSpecification(item.Buyer.BuyerId);
            item.Buyer.UnproccessedOrdersCount = await orderRepository.CountAsync(countSpec);
        }
        return buyers;
    }

    public async Task<Order> ApproveOrderByIdAsync(int id)
    {
        var order = await GetOrderByIdAsync(id);
        order.IsInProcess = false;
        await orderRepository.UpdateAsync(order);
        return order;
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}