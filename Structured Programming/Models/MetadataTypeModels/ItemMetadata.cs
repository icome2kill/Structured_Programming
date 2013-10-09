using System;
using System.ComponentModel.DataAnnotations;

namespace Structured_Programming.Models
{
    [MetadataType(typeof(ItemMetadata))]
    public partial class Item
    {
    }
    public class ItemMetadata
    {
        [Required]
        [StringLength(50)]
        public Object Name { get; set; }

        [Required]
        public Object Description { get; set; }

        [StringLength(50)]
        public Object Trade { get; set; }

        [Range(0, 100000000000, ErrorMessage = "Price must be in range {1}, {2}")]
        [DataType(DataType.Currency)]
        public Object Price { get; set; }
    }
}