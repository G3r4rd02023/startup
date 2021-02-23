using startup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup.Helpers
{
    public class CombosHelper:IDisposable
    {
        private static StartupContext db = new StartupContext();

        public static List<Country> GetCountries()
        {
            var countries = db.Countries.ToList();
            countries.Add(new Country
            {
                CountryId = 0,
                Name = "[Selecciona un país...]",
            });

            return countries.OrderBy(d => d.Name).ToList();

        }

        public static List<City> GetCities()
        {
            var cities = db.Cities.ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[Selecciona una Ciudad...]",
            });

            return cities.OrderBy(c => c.Name).ToList();

        }

        public static List<Company> GetCompanies()
        {
            var companies = db.Companies.ToList();
            companies.Add(new Company
            {
                CompanyId = 0,
                Name = "[Seleccione una empresa...]",
            });

            return companies.OrderBy(c => c.Name).ToList();

        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}