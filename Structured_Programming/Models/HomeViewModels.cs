using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Structured_Programming.Models
{
    public class HomeIndexModel
    {
        public IEnumerable<Structured_Programming.Models.Item> NewestItems { get; set; }
        public IEnumerable<Structured_Programming.Models.UserProfile> TopUsers { get; set; }
    }
}