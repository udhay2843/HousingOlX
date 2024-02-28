using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data.Interfaces
{
    public interface IFurnishingTypeRepository
    {
        public Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync();
    }
}