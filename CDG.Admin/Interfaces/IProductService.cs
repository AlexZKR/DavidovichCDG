using CDG.Admin.Models.Product;

namespace CDG.Admin.Interfaces;
public interface IProductService : IBaseService
{
    Task<T> CountKeys<T>();
    Task<T> GetKeyById<T>(int id);
    Task<T> GetKeysPaged<T>(int page, int pageSize);
    Task<T> AddKey<T>(ProductDTO Key);
    Task<T> UpdateKey<T>(ProductDTO Key);
    Task<T> DeleteKey<T>(int id);
    Task<T> GetCategories<T>();
}