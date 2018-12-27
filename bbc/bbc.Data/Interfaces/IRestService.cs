using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bbc.Data.Interfaces
{
    public interface IRestService<T>
    {
        Task<List<T>> GetDataAsync();
        Task<bool> PostDataAsync(T obj);
        Task<bool> PutDataAsync(T obj, string id);
        Task<bool> DeleteDataAsync(string id);
    }
}
