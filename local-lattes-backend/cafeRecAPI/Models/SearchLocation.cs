using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static cafeRecAPI.Models.CafeLocation;

namespace cafeRecAPI.Models
{
    public class SearchLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Location { get; set; }

        [JsonIgnore]
        public virtual ICollection<CafeSearchLocation> CafeSearchLocations { get; set; }

    }
}
