using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Structured_Programming.Models
{
    public class TransactionCreateModel
    {
        public Item Item { get; set; }
        public IEnumerable<Item> MyItems { get; set; }
        public Transaction Transaction { get; set; }
        public SelectList MethodList { get; set; }
    }
}