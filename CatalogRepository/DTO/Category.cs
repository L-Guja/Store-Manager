using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogRepository.DTO
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(15)]
        public string CategoryName { get; set; } = null!;
    }
}
