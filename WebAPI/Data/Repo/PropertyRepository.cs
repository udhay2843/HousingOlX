using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        public readonly Dbclass db;
        public PropertyRepository(Dbclass db)
        {
            this.db = db;
            
        }
        public void AddProperty(Property property)
        {
            db.Properties.Add(property);
        }

        public void DeleteProperty(int id)
        {
           var property = db.Properties.Find(id);
             if(property !=null)
                { 
                      db.Properties.Remove(property);

                }
            
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            var properties=await db.Properties
            .Include(p=>p.PropertyType)
            .Include(p=>p.FurnishingType)
            .Include(p=>p.Photos)
            .Include(p=>p.City)
            .Where(p=>p.SellRent==sellRent)
            .ToListAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailsAsync(int id)
        {
           var properties=await db.Properties
            .Include(p=>p.PropertyType)
            .Include(p=>p.FurnishingType)
            .Include(p=>p.Photos)
            .Include(p=>p.City)
            .Where(p=>p.Id==id)
            .FirstAsync();
            return properties;
        }
        public async Task<Property> GetPropertyByIdAsync(int id)
        {
           var properties=await db.Properties
            .Include(p=>p.Photos)
            .Where(p=>p.Id==id)
            .FirstOrDefaultAsync();
            return properties;
        }
    }
}