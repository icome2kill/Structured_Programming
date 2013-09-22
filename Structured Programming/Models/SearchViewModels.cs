using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Structured_Programming.Models
{
    public class SearchResultModel {
        public IEnumerable<Structured_Programming.Models.Item> Items {get; set;}
        public IEnumerable<Structured_Programming.Models.UserProfile> Users {get; set;}

        public SearchOptionsModel CurrentOptions { get; set; }
    }
    public class SearchOptionsModel
    {
        [Required]
        public string Query { get; set; }

        public bool InclName { get; set; }
        public bool InclUser { get; set; }
        public bool InclDescription { get; set; }
        public bool InclTrade { get; set; }

        public int TypeId { get; set; }
        public SelectList TypeList { get; set; }
    }
}