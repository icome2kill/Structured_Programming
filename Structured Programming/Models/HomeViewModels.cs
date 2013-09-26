using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Structured_Programming.Models
{
    public class HomeIndexModel
    {
        public int TypeId { get; set; }
        public IEnumerable<Structured_Programming.Models.Item> LastestItems { get; set; }
        public IEnumerable<Structured_Programming.Models.Item> Items { get; set; }
    }
}