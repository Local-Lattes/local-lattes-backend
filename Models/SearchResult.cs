namespace cafeRecAPI.Models
{
    public class SearchResultCS
    {
        public string Match { get; set; }
        public int LevenshteinDistance { get; set; }
    }
}
