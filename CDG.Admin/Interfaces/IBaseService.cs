using CDG.Admin.Models;

namespace CDG.Admin.Interfaces;

public interface IBaseService : IDisposable
{
    ResponseDTO responseModel { get; set; }
    Task<T> SendAsync<T>(APIRequest request);
}