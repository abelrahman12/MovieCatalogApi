using System.ComponentModel.DataAnnotations;

namespace MoviCatalogApi.Dtos.CategoryDto
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
