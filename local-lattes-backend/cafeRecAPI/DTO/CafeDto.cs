using System;
using cafeRecAPI.DTO;

namespace cafeRecAPI.DTO
{
	public class CafeDto
	{
		public int Id { get; set; }
		public string CafeName { get; set; }
		public string Address { get; set; }
		public string PlaceId { get; set; }
		public double Rating { get; set; }
		public IEnumerable<ReviewDto> Reviews { get; set; }
	}
}

