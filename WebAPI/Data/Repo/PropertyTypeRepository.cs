using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly Dbclass db;
        public PropertyTypeRepository(Dbclass db)
        {
            this.db = db;
            
        }
        public async Task<IEnumerable<PropertyType>> GetPropertyTypeAsync()
        {
           return await db.PropertyTypes.ToListAsync();
        }
    }
}