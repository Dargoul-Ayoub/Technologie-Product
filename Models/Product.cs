using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechProduct.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [MaxLength(100, ErrorMessage = "The Name is too long")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Name it's Required")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "The Description is too long")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Description it's Required")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The price it's Required")]
        public decimal Price { get; set; }

        [MaxLength(100, ErrorMessage = "The Catergory is too long")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Name it's Required")]
        public string Category { get; set; }
        public byte[] ImageData { get; set; }
        [MaxLength(50)]
        public string ImageMimType { get; set; }
    }
}
