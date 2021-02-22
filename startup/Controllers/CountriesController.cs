using startup.Helpers;
using startup.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace startup.Controllers
{
    public class CountriesController : Controller
    {
        private StartupContext db = new StartupContext();

        public ActionResult AddCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var country = db.Countries.Find(id);

            if (country == null)
            {
                return HttpNotFound();
            }

            City model = new City { CountryId = country.CountryId };
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCity(City city)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Country country = db.Countries
                           .Include(c => c.Cities)
                           .FirstOrDefault(c => c.CountryId == city.CountryId);

                    country.Cities.Add(city);

                    var response = DBHelper.SaveChanges(db);
                    
                    if (!response.Succedeed)
                    {
                        ModelState.AddModelError(string.Empty, response.Message);
                    }
                    return RedirectToAction($"{nameof(Details)}/{country.CountryId}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(city);

        }

        public ActionResult EditCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            City city = db.Cities.Find(id);
            
            if (city == null)
            {
                return HttpNotFound();
            }

            var country = db.Countries.FirstOrDefault(c => c.Cities.FirstOrDefault(p => p.CountryId == city.CountryId) != null);
            city.CountryId = country.CountryId;

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCity(City city)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(city).State = EntityState.Modified;
                    var response = DBHelper.SaveChanges(db);
                    if (!response.Succedeed)
                    {
                        ModelState.AddModelError(string.Empty, response.Message);
                    }
                    return RedirectToAction($"{nameof(Details)}/{city.CountryId}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(city);
        }

        public ActionResult DeleteCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = db.Cities.Find(id);

            if (city == null)
            {
                return HttpNotFound();
            }

            db.Cities.Remove(city);
            db.SaveChanges();
            return RedirectToAction($"{nameof(Details)}/{city.CountryId}");

        }

        // GET: Countries
        public ActionResult Index()
        {
            return View(db.Countries.ToList());
        }

        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Country country = db.Countries.Find(id);

            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Countries.Add(country);
                    var response = DBHelper.SaveChanges(db);
                    if (response.Succedeed)
                    {                      
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, response.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
            }

            return View(country);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Country country = db.Countries.Find(id);

            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succedeed)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Country country = db.Countries.Find(id);

            if (country == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.Countries.Remove(country);
                var response = DBHelper.SaveChanges(db);
                if (response.Succedeed)
                {
                    return RedirectToAction("Index");                    
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(country);
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
