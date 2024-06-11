using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogRepository.DTO
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; } = null!;

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        [Column(TypeName = "smallint")]
        public short? UnitsInStock { get; set; }

        [Column(TypeName = "smallint")]
        public short? UnitsOnOrder { get; set; }

        [Column(TypeName = "smallint")]
        public short? ReorderLevel { get; set; }

        [Required]
        public bool Discontinued { get; set; }
    }
}
