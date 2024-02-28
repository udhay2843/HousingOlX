using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interface;

namespace WebAPI.Data.Interfaces
{
    public interface IUnitofWork
    {
        ICityRepository cityRepository{get;}
        IUserRepository userRepository{get;}
        IPropertyRepository propertyRepository {get;}
        IPropertyTypeRepository propertyTypeRepository{get;}
        IFurnishingTypeRepository furnishingTypeRepository{get;}
        Task<bool> SaveAsync();
    }
}