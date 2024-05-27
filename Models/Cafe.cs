using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static cafeRecAPI.Models.CafeLocation;

namespace cafeRecAPI.Models
{
    public class Cafe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? CafeName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string PlaceId { get; set; }
        public double Rating { get; set; }
        public virtual ICollection<Review> Reviews { get; } = new List<Review>();
        [JsonIgnore]
        public virtual ICollection<CafeSearchLocation> CafeSearchLocations { get; set; }
    }
}
