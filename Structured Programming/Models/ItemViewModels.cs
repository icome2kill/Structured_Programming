using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Structured_Programming.Models
{
    public class ItemIndexModel
    {
        public IEnumerable<Structured_Programming.Models.Item> Items { get; set; }
    }
    public class ItemEditModel
    {
        public SelectList TypeList { get; set; }
        public Item Item { get; set; }
    }
    public class ItemAddModel
    {
        public SelectList TypeList {get;set;}
        [Required]
        [Display(Name = "Name")]
        public string ItemName { get; set; }

        [Display(Name = "Description")]
        public string ItemDescription { get; set; }

        [Display(Name = "Price")]
        public decimal ItemPrice { get; set; }

        [Display(Name = "Want to trade with")]
        public string ItemTradeWith { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int ItemTypeId { get; set; }
    }
}