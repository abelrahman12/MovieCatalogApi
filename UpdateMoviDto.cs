namespace MoviCatalogApi.Dtos.MoviDto
{
    public class UpdateMoviDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; } 
        public int Rating { get; set; }
        public string Director { get; set; }
    }
}
