using Structured_Programming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Structured_Programming.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            using (DataEntities db = new DataEntities())
            {
                this.ViewData["Categories"] = db.Types.ToList();
            }
        }
    }
}
