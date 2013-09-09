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
    public class ItemFormModel
    {
        public SelectList TypeList { get; set; }
        public Item Item { get; set; }
    }
}