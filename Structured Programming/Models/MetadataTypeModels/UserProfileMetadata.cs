using System;
using System.ComponentModel.DataAnnotations;

namespace Structured_Programming.Models
{
    [MetadataType(typeof(UserProfileMetadata))]
    public partial class UserProfile
    {
        public double Rating { get{
            return Structured_Programming.Models.Business.AccountsManagement.Rating(this);
        }}
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