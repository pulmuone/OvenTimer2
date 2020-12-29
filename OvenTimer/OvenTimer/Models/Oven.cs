namespace OvenTimer.Models
{
    public class Oven
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }

        public int OvenNo { get; set; }
    }
}
