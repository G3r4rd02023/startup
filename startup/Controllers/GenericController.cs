using startup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup.Controllers
{
    public class GenericController : Controller
    {
        // GET: Generic
        private StartupContext db = new StartupContext();
        // GET: Generic
        public JsonResult GetCities(int countryId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(c => c.CountryId == countryId);
            return Json(cities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}