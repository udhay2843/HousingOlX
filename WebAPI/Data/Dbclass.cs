using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class Dbclass:DbContext
    {
        public Dbclass(DbContextOptions<Dbclass> options):base(options){}
        public DbSet<City>? Cities {get; set;}
         public DbSet<User>? Users {get; set;}
           public DbSet<Property> Properties {get; set;}
             public DbSet<PropertyType>? PropertyTypes {get; set;}
               public DbSet<FurnishingType>? FurnishingTypes {get; set;}

    }
}