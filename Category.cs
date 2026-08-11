using System.ComponentModel.DataAnnotations;
namespace MoviCatalogApi.Models

{
    public class Category
    {
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Movi>Movis = new List<Movi>();

    }
}
