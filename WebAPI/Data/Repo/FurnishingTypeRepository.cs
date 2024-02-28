using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly Dbclass db;
        public FurnishingTypeRepository(Dbclass db)
        {
            this.db=db;
        }
        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
           return await db.FurnishingTypes.ToListAsync();
        }
    }
}