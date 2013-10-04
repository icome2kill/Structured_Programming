using System;
using System.ComponentModel.DataAnnotations;

namespace Structured_Programming.Models
{
    [MetadataType(typeof(webpages_RolesMetadata))]
    public partial class webpages_Roles
    {
    }
    public class webpages_RolesMetadata
    {
        [Key]
        public int RoleId { get; set; }
    }
}