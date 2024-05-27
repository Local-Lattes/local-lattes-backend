using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace cafeRecAPI.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? ReviewText { get; set; }
        public int? CafeId { get; set; }

        [JsonIgnore]
        public virtual Cafe? Cafe { get; set; }
        
        
    }
}