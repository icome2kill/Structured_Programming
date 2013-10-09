using Structured_Programming.Filters;
using Structured_Programming.Models;
using Structured_Programming.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Structured_Programming.Controllers
{
    [InitializeSimpleMembership]
    public class RateController : BaseController
    {
        // When an user rates another one, it creates a "rate" entry.
        // POST: /Rate/Create

        [HttpPost]
        public ActionResult Create(int userId, int score = 0)
        {
            try
            {
                // TODO: Add insert logic here
                // Cannot rate oneself...
                // Rating score should be between 1 and 5
                if (WebSecurity.CurrentUserId == userId || score < 1 || score > 5)
                {
                    return RedirectToAction("Details", "Account", new { id = userId });
                }
                using (DataEntities db = new DataEntities())
                {
                    if (!db.UserProfiles.Select(m => m.UserId).Contains(userId))
                    {
                        return View("Error");
                    }
                    // Limit to 1 vote per day
                    if (!RatingManagement.CanRate(WebSecurity.CurrentUserId, userId, 1))
                    {
                        return View("Error");
                    };
                    Rate rating = new Rate() {
                        UserId = WebSecurity.CurrentUserId,
                        RatedUserId = userId,
                        Score = score,
                        Date = DateTime.Now
                    };
                    db.Rates.Add(rating);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Account", new { id = userId });
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
