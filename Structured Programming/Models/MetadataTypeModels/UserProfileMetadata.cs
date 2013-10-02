using System;
using System.ComponentModel.DataAnnotations;

namespace Structured_Programming.Models
{
    [MetadataType(typeof(UserProfileMetadata))]
    public partial class UserProfile
    {
    }

    public class UserProfileMetadata
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(50)]
        public Object FirstName { get; set; }

        [StringLength(50)]
        public Object LastName { get; set; }
    }
}