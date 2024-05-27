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
        private readonly ICafeService cafeService;
        private readonly ICafeRepo _repository;



        public CafeApiController(ILogger<CafeApiController> logger, ICafeService CafeService, ICafeRepo repository)
        {
            _logger = logger;
            cafeService = CafeService;
            _repository = repository;
        }

        [HttpGet("GetCafeList")]
        public async Task<List<Cafe>> GetCafeList(string location)
        {
            var cafes = await cafeService.GetCafesAsync(location);
            return cafes;
        }





        [HttpGet("GetCafeListHardCoded")]
        public async Task<List<Cafe>> GetCafeListTEST()
        {
            var cafes = await cafeService.GetCafesAsync("Auckland CBD auckland central");


            return cafes;
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
    
        
        
        
        public class ServerOsInfo
        {
            public string OsDescription { get; set; }
            public Architecture OsArchitecture { get; set; }
            public string FrameworkDescription { get; set; }
        }

        [HttpGet("OsInfo")]
        public ActionResult<ServerOsInfo> ServerOs()
        {
            var osInfo = new ServerOsInfo
            {
                OsDescription = RuntimeInformation.OSDescription,
                OsArchitecture = RuntimeInformation.OSArchitecture,
                FrameworkDescription = RuntimeInformation.FrameworkDescription
            };

            return Ok(osInfo);
        }

    }
}
