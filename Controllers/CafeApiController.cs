using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cafeRecAPI.DTO;
using cafeRecAPI.Models;
using cafeRecAPI.Infra;
using cafeRecAPI.Data;
using System.Runtime.InteropServices;
using cafeRecAPI.Service;

namespace cafeRecAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CafeApiController : ControllerBase
    {
        private readonly ILogger<CafeApiController> _logger;
        private readonly ICafeService _cafeService;
        private readonly ICafeRepo _repository;
        public CafeApiController(ILogger<CafeApiController> logger, ICafeService CafeService, ICafeRepo repository)
        {
            _logger = logger;
            _cafeService = CafeService;
            _repository = repository;
        }
        [HttpGet("GetCafeList")]
        public async Task<List<Cafe>> GetCafeList(string location)
        {
            var cafes = await _cafeService.GetCafesAsync(location);
            return cafes;
        }
        [HttpGet("GetCafeListHardCoded")]
        public async Task<List<Cafe>> GetCafeListTEST()
        {
            return await _cafeService.GetCafesAsync("Auckland CBD auckland central");
        }
        [HttpGet("AucklandCBDcafes")]
        public async Task<IEnumerable<Cafe>> test()
        {
            var cafes = _repository.GetCafeByLocation("Auckland CBD");
            Console.WriteLine(_repository.GetAllCafes());
            return cafes;
        }
        [HttpGet("CafeById")]
        public async Task<Cafe> test2()
        {
            var cafe = _repository.GetCafeById(2);
            return cafe;
        }
    }
}
