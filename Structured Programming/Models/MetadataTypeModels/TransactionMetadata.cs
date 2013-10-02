using System;
using System.ComponentModel.DataAnnotations;

namespace Structured_Programming.Models 
{
    [MetadataType(typeof(TransactionMetadata))]
    public partial class Transaction
    {

    }

    public class TransactionMetadata
    {
        [Range(0, Double.MaxValue, ErrorMessage="Your offer must be greater than 0")]
        public Object Pay { get; set; }
    }
}