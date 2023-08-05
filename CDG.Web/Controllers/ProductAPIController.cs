using AutoMapper;
using CDG.Web.Models;
using CDG.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CDG.Web.Models.DigitalKey;
using CDG.Web.Infrastructure;
using CDG.BLL.Entities.Products;
using CDG.Web.Models.KeyCategory;

namespace CDG.Web.Controllers;
[Route("api/DigitalKeys")]

public class ProductAPIController : ControllerBase
{
    private readonly IDigitalKeyCatalogService DigitalKeyService;
    private readonly IMapper mapper;
    private readonly IAppLogger<ProductAPIController> logger;
    protected ResponseDTO response;

    public ProductAPIController(IDigitalKeyCatalogService DigitalKeyService,
     IMapper mapper,
     IAppLogger<ProductAPIController> logger)
    {
        response = new ResponseDTO();
        this.DigitalKeyService = DigitalKeyService;
        this.mapper = mapper;
        this.logger = logger;
    }

    [HttpGet]
    [QueryParameterConstraint("id")]
    public async Task<ActionResult<object>> GetDigitalKey([FromQuery] int id)
    {
        try
        {
            var DigitalKey = await DigitalKeyService.GetDigitalKeyAsync(id);
            var dto = mapper.Map<DigitalKeyDTO>(DigitalKey);
            response.Result = dto;
            logger.LogInformation($"Got DigitalKey {id}");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"GetDigitalKey {id} ex: {ex.Message}");
        }
        response.DisplayMessage = $"GetDigitalKey id: {id}";
        return response;
    }

    [HttpGet("KeyCategorys")]
    public async Task<ActionResult<object>> GetKeyCategorys()
    {
        try
        {
            var KeyCategorys = await DigitalKeyService.GetKeyCategorys();
            var dtos = mapper.Map<List<KeyCategoryDTO>>(KeyCategorys);
            response.Result = dtos;
            logger.LogInformation($"Action {nameof(GetKeyCategorys)} Sent: {dtos.Count} entities");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"Action {nameof(GetKeyCategorys)} ex: {ex.Message}");
        }
        response.DisplayMessage = $"Action {nameof(GetKeyCategorys)}";
        return response;
    }

    [HttpGet]
    [QueryParameterConstraint("page", "pageSize")]
    public async Task<ActionResult<object>> GetDigitalKeysPaged([FromQuery] int page, int pageSize)
    {
        try
        {
            var DigitalKeys = await DigitalKeyService.GetDigitalKeysPaged(page, pageSize);
            var dtos = mapper.Map<List<DigitalKeyDTO>>(DigitalKeys);
            response.Result = dtos;
            logger.LogInformation($"Action {nameof(GetDigitalKeysPaged)} page: {page}; pageSize: {pageSize}; Sent {dtos.Count} DigitalKeys");

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"Action {nameof(GetDigitalKeysPaged)} page: {page}; pageSize: {pageSize}; ex: {ex.Message}");

        }
        response.DisplayMessage = $"Action {nameof(GetDigitalKeysPaged)} pageNo: {page}; pageSize {pageSize}";
        return response;
    }

    [HttpGet("count")]
    [ActionName("count")]
    public async Task<ActionResult<object>> CountAsync()
    {
         try
        {
            var DigitalKeysCount = await DigitalKeyService.CountDigitalKeysAsync();
            // var dto = mapper.Map<CountDataDTO>(DigitalKeysCount);
            response.Result = new CountDataDTO {DigitalKeysTotal = DigitalKeysCount};
            logger.LogInformation($"Action {nameof(CountAsync)} Got {DigitalKeysCount} DigitalKeys in DB");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogInformation($"Action {nameof(CountAsync)} Som problem");
        }
        return response;
    }

    [HttpPost("add")]
    public async Task<ActionResult<object>> AddDigitalKeyAsync([FromBody] DigitalKeyDTO DigitalKeyDTO)
    {
        try
        {
            logger.LogInformation($"Received {DigitalKeyDTO.Name}, {DigitalKeyDTO.Description}");
            var model = mapper.Map<DigitalKey>(DigitalKeyDTO);
            var DigitalKey = await DigitalKeyService.AddDigitalKeyAsync(model);
            var dtoResponse = mapper.Map<DigitalKeyDTO>(DigitalKey);
            response.Result = dtoResponse;
            logger.LogWarning($"Action {nameof(AddDigitalKeyAsync)} added DigitalKey id: {DigitalKey.Id}");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"Action {nameof(AddDigitalKeyAsync)} ex: {ex.Message}");

        }
        response.DisplayMessage = $"Action {nameof(AddDigitalKeyAsync)}";
        return response;
    }


    [HttpPost("update")]
    public async Task<ActionResult<object>> UpdateDigitalKeyAsync([FromBody] DigitalKeyDTO DigitalKeyDTO)
    {
        try
        {
            logger.LogInformation($"Received {DigitalKeyDTO.Name}, {DigitalKeyDTO.Description}, id {DigitalKeyDTO.Id} for updating");
            var model = mapper.Map<DigitalKey>(DigitalKeyDTO);
            var DigitalKey = await DigitalKeyService.UpdateDigitalKeyAsync(model);
            var dtoResponse = mapper.Map<DigitalKeyDTO>(DigitalKey);
            response.Result = dtoResponse;
            logger.LogWarning($"Action {nameof(UpdateDigitalKeyAsync)} updated DigitalKey id: {DigitalKey.Id}");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"Action {nameof(UpdateDigitalKeyAsync)} ex: {ex.Message}");
        }
        response.DisplayMessage = $"Action {nameof(UpdateDigitalKeyAsync)}";
        return response;
    }
    [HttpDelete("delete")]
    [QueryParameterConstraint("id")]
    public ActionResult<object> DeleteDigitalKeyAsync([FromQuery] int id)
    {
        try
        {
            logger.LogInformation($"Received DigitalKey id {id} to delete");
            DigitalKeyService.DeleteDigitalKeyAsync(id);
            logger.LogWarning($"Action {nameof(DeleteDigitalKeyAsync)} deleted DigitalKey id: {id}");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"Action {nameof(DeleteDigitalKeyAsync)} ex: {ex.Message}");
        }
        response.DisplayMessage = $"Action {nameof(DeleteDigitalKeyAsync)}";
        return response;
    }
}