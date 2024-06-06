namespace cafeRecAPI.Models
{
    public class Address
    {
        public int CafeId { get; set; }
        public virtual Cafe Cafe { get; set; }
        public int SearchLocationId { get; set; }
        public virtual SearchLocation SearchLocation { get; set; }

    }
}
