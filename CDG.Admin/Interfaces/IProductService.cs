using CDG.Admin.Models.Product;

namespace CDG.Admin.Interfaces;
public interface IProductService : IBaseService
{
    Task<T> CountBooks<T>();
    Task<T> GetBookById<T>(int id);
    Task<T> GetBooksPaged<T>(int page, int pageSize);
    Task<T> AddBook<T>(ProductDTO book);
    Task<T> UpdateBook<T>(ProductDTO book);
    Task<T> DeleteBook<T>(int id);
    Task<T> GetAuthors<T>();
}