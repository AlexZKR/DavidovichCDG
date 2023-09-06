using AutoMapper;
using CDG.Admin.Extensions;
using CDG.Admin.Infrastructure;
using CDG.Admin.Interfaces;
using CDG.Admin.Models;
using CDG.Admin.Models.Product;
using CDG.Admin.ViewModels.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CDG.Admin.Controllers;
public class ProductsController : Controller
{
    private readonly IProductService productService;
    private readonly IMapper mapper;
    private readonly ILogger<ProductsController> logger;

    public ProductsController(IProductService productService,
    IMapper mapper,
    ILogger<ProductsController> logger)
    {
        this.productService = productService;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new ProductsPageViewModel();

        var response = await productService.GetKeysPaged<ResponseDTO>(0, SD.PageSize);
        if (response != null && response.IsSuccess)
        {
            var list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result)!);
            vm.Products = list!.Select(d => mapper.Map<ProductViewModel>(d)).OrderByDescending(x => x.Name).ToList();
            vm.Pagination.ActualPage = 0;
            vm.Pagination.ItemsPerPage = SD.PageSize;
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            vm.IsSuccess = false;
            return View(vm);
        }

        if (vm.Products.Count == 0) vm.StatusMessage = "Nothing found";

        return View(vm);
    }

    [HttpGet("GetProductsPage")]
    public async Task<ActionResult<object>> GetProductsPage(int page)
    {
        var response = await productService.GetKeysPaged<ResponseDTO>(page, SD.PageSize);
        if (response != null && response.IsSuccess)
        {
            logger.LogInformation($"Sending request for page {page}");
            var list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result)!);
            return PartialView("_ProductListPartial", list!.Select(d => mapper.Map<ProductViewModel>(d)).OrderByDescending(x => x.Name).ToList());
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    [Route("Products/PaginationInfo")]
    public async Task<ActionResult<object>> PaginationInfo()
    {
        logger.LogInformation("Sending page info");
        var response = await productService.CountKeys<ResponseDTO>();
        if (response != null && response.IsSuccess)
        {
            var dto = JsonConvert.DeserializeObject<CountDataDTO>(Convert.ToString(response.Result)!);
            logger.LogInformation($"There are {dto!.KeysTotal} Keys.");

            return Json(new {
                count = dto!.KeysTotal,
                pageSize = SD.PageSize
            });
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("KeyInfo")]
    public async Task<IActionResult> KeyInfo([FromQuery] int itemId)
    {
        var vm = new ProductViewModel();

        var response = await productService.GetKeyById<ResponseDTO>(itemId);
        if (response != null && response.IsSuccess)
        {
            var Key = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result)!);
            vm = mapper.Map<ProductViewModel>(Key);
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            vm.IsSuccess = false;
            return View(vm);
        }

        if (this.Request.IsAjaxRequest())
            return PartialView("_ProductInfoPartial", vm);
        else
            return View("_ProductInfoPartial", vm);
    }

    [HttpDelete]
    [QueryParameterConstraintAttribute("itemId")]
    public async Task<ActionResult<object>> KeyDelete([FromQuery] int itemId)
    {
        var response = await productService.DeleteKey<ResponseDTO>(itemId);
        if (response != null && response.IsSuccess)
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    Success = response.IsSuccess,
                    Id = itemId
                });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        else
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    success = false,
                });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var vm = new ProductViewModel();

        var response = await productService.GetKeyById<ResponseDTO>(id);
        if (response != null && response.IsSuccess)
        {
            var Key = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result)!);
            vm = mapper.Map<ProductViewModel>(Key);
            vm.Categories = (await GetCategoriesSelectList()).ToList();
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            vm.IsSuccess = false;
            return View(vm);
        }
            return View("ProductEdit", vm);
    }

    [HttpPost("KeyEdit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> KeyEdit(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = mapper.Map<ProductDTO>(model);
            if(Request.Form.Files["picture"] != null)
                dto.PictureUri = Path.GetFileName(Request.Form.Files["picture"]!.FileName);
            if(dto.Discount > 1)
                dto.Discount = dto.Discount / 100;
            logger.LogInformation("Filename: " + dto.PictureUri);
            await productService.UpdateKey<ResponseDTO>(dto);
            return RedirectToAction(nameof(Index));
        }
        model = await PopulateVMSelectLists(model);
        return View("ProductEdit",model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var vm = await PopulateVMSelectLists(new ProductViewModel());
        return View("ProductCreate", vm);
    }

    [HttpPost("KeyCreate")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> KeyCreate(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = mapper.Map<ProductDTO>(model);
             if(Request.Form.Files["picture"] == null)
                dto.PictureUri = "no_img.jpg";
            else
                dto.PictureUri = Path.GetFileName(Request.Form.Files["picture"]!.FileName);
            if(dto.Discount > 1)
                dto.Discount = dto.Discount / 100;
            await productService.AddKey<ResponseDTO>(dto);
            return RedirectToAction(nameof(Index));
        }
        model = await PopulateVMSelectLists(model);
        return View("ProductCreate",model);
    }

    private async Task<IEnumerable<SelectListItem>> GetCategoriesSelectList()
    {
        logger.LogInformation("GetCategories called");
        var response = await productService.GetCategories<ResponseDTO>();
        var authorNames = JsonConvert.DeserializeObject<List<KeyCategoryDTO>>(Convert.ToString(response.Result)!);
        var items = authorNames!.Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString()})
            .OrderBy(n => n.Text)
            .ToList();

        var allItem = new SelectListItem() { Value = "0", Text = "Не определен", Selected = true };
        items.Insert(0, allItem);
        return items;
    }

    private async Task<ProductViewModel> PopulateVMSelectLists(ProductViewModel vm)
    {
        vm.Tags = EnumHelper<Tag>.GetStaticDataFromEnum(Tag.None).ToList();
        vm.Categories = (await GetCategoriesSelectList()).ToList();
        return vm;
    }
}