using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using cafeRecAPI.DTO;
using cafeRecAPI.Data;
using cafeRecAPI.Models;
using cafeRecAPI.Infra;
using Microsoft.EntityFrameworkCore;
using static cafeRecAPI.Models.CafeLocation;
using cafeRecAPI.Service;

class CafeService : ICafeService
{
    private readonly CafeDBContext CafeDBContext;
    private readonly ICafeRepo _repository;

    public CafeService(CafeDBContext cafeDbContext, ICafeRepo repository)
    {
        CafeDBContext = cafeDbContext;
        _repository = repository;
    }

    public async Task<List<Cafe>> GetCafesAsync(string location)
    {
        try
        {
            var httpClient = new HttpClient();
            var response = new List<Cafe>();
            
            //implement fuzzy search later 
            var existingLocation = CafeDBContext.Locations.FirstOrDefault(l => l.Location == location);
            if (existingLocation == null)
            {
                // If the location doesn't exist, create a new SearchLocation instance
                var newLocation = new SearchLocation { Location = location };
                CafeDBContext.Locations.Add(newLocation);
                CafeDBContext.SaveChanges(); // Save changes to generate the Id
                existingLocation = newLocation;
            }
            else
            {
                Console.WriteLine("Success");
                return (List<Cafe>)_repository.GetCafeByLocation(location);
            }

            return response;

            //deleted old code
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<Cafe>();
        }

    }
}


