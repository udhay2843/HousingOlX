using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly Dbclass db;
        public CityRepository(Dbclass db)
        {
            this.db=db;
        }
        public void AddCity(City city)
        {
           db.Cities.Add(city);
        }

        public async Task<City> FindCity(int id)
        {
            return await db.Cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await db.Cities.ToListAsync();
        }

        public void RemoveCity(int id)
        {
            var city=db.Cities.Find(id);
            db.Cities.Remove(city);
        }

       
    }
}