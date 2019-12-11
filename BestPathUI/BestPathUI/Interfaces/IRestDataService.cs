using System.Threading.Tasks;
using System.Collections.Generic;
using Models;
using Models.Interfaces;

namespace Interfaces
{
    public interface IRestDataService<T>
        where T : class, IBaseModel
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IList<T>> GetItemsAsync();
        Task<IList<T>> GetItemsAsync(string filters);
        Task<IList<BaseModel>> GetBaseNameModel();
        Task<TResult> ReturnGetHttp<TResult>(string url);
    }
}

