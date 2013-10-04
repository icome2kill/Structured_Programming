using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Structured_Programming.Models.Business
{
    public class AccountsManagement
    {
        public static double Rating(UserProfile user)
        {
            double total = 0;
            var rows = user.Rated.ToList();
            foreach (var row in rows) {
                total += row.Score;
            }
            return rows.Count == 0? 0:total/rows.Count;
        }
    }
}