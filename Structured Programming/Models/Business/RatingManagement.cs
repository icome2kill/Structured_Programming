using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Structured_Programming.Models.Business
{
    public class RatingManagement
    {
        // return whether a user can rate others base on  his last time rate.
        public static Boolean CanRate(int raterId, int ratedId, double limit)
        {
            using (DataEntities db = new DataEntities())
            {
                var row = db.Rates.Where(m => m.UserId == raterId && m.RatedUserId == ratedId).OrderByDescending(m => m.Date).FirstOrDefault();
                if (row == null)
                {
                    return true;
                }
                return row.Date.CompareTo(DateTime.Now) == 1;
            }
        }
    }
}