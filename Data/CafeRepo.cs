//implement
using cafeRecAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace cafeRecAPI.Data
{
	public class CafeRepo : ICafeRepo
	{
		private readonly CafeDBContext _dbContext;
        public CafeRepo(CafeDBContext dbContext)
		{
			_dbContext = dbContext;
        }
        public IEnumerable<Cafe> GetAllCafes()
		{
			return _dbContext.Cafes.ToList();
		}
        public IEnumerable<Cafe> GetCafeByLocation(string location)
        {
            List<string> locations = (List<string>)this.GetAllLocationsString(); //fuzzy search later
            var cafeLocation = _dbContext.Locations.FirstOrDefault(x => x.Location == location);
            if (cafeLocation != null)
            {
                var cafeSearchLocations = _dbContext.CafeSearchLocations.Where(c => c.SearchLocationId == cafeLocation.Id).Select(c => c.Cafe).ToList();
                return cafeSearchLocations;
            }
            else
            {
                return Enumerable.Empty<Cafe>(); // Return an empty collection if the location is not found
            }
        }
        public Cafe GetCafeById(int id)
		{
			return _dbContext.Cafes.FirstOrDefault(c => c.Id == id);
		}
		public bool CafeExists(int id)
		{
			return _dbContext.Cafes.Any(c => c.Id == id);
		}
        public IEnumerable<SearchLocation> GetAllLocations()
        {
            return _dbContext.Locations;
        }
        public IEnumerable<string> GetAllLocationsString()
        {
            List<string> temp = new List<string>();
            IEnumerable <SearchLocation> temp2 = GetAllLocations();
            foreach(SearchLocation i in temp2)
            {
                temp.Add(i.Location);
            }
            return temp;
        }
    }
}
