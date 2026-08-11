using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviCatalogApi.Dtos.MoviDto
{
    public class CreatMoviDto
    {
        [Required, MaxLength(150)]
        public string Title { get; set; }
        [Required, MaxLength(100)]
        public string Director { get; set; }
        [Range(1950, 2035)]
        public int ReleaseYear { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required, Range(1, 10)]
        public int Rating { get; set; }
    }
}
