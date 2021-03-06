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
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Items = new HashSet<Item>();
            this.webpages_Roles = new HashSet<webpages_Roles>();
            this.Transactions = new HashSet<Transaction>();
            this.Rated = new HashSet<Rate>();
            this.Rates = new HashSet<Rate>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Rate> Rated { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
