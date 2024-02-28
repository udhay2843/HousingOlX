using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Interfaces;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly Dbclass db;
        public UnitOfWork(Dbclass db)
        {
            this.db=db;
        }
        public ICityRepository cityRepository=>
        new CityRepository(db);

        public IUserRepository userRepository =>
        new UserRepository(db);

        public IPropertyRepository propertyRepository =>
        new PropertyRepository(db);

        public IPropertyTypeRepository propertyTypeRepository => 
        new PropertyTypeRepository(db);

        public IFurnishingTypeRepository furnishingTypeRepository => 
        new FurnishingTypeRepository(db);

        public async Task<bool> SaveAsync()
        {
            return await db.SaveChangesAsync()>0;
        }
    }
}