//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Structured_Programming.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RatedUserId { get; set; }
        public int Score { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
    }
}
