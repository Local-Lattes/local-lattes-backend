using System.Collections.Generic;
using System.Threading.Tasks;
using cafeRecAPI.DTO;
using cafeRecAPI.Infra;
using cafeRecAPI.Models;

namespace cafeRecAPI.Service
{
    public interface ICafeService
    {
        Task<List<Cafe>> GetCafesAsync(string location);
    }
}