using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cafeRecAPI.Models;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
namespace cafeRecAPI.Data
{
	public interface ICafeRepo
	{
		public IEnumerable<Cafe> GetAllCafes();
		public Cafe GetCafeById(int id);
		public bool CafeExists(int id);
		//public IEnumerable<Cafe> GetCafeByTags(IEnumerable<string> tags);
		public IEnumerable<Cafe> GetCafeByLocation(string location);
		public IEnumerable<SearchLocation> GetAllLocations();
        public IEnumerable<string> GetAllLocationsString();
    }
}







   