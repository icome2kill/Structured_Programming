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
        public Object Name { get; set; }

        [StringLength(50)]
        public Object Trade { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage="Price must be greater than 0")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.0}")]
        public Decimal Price { get; set; }
    }
}