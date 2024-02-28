using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Interfaces;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.Controllers
{
  [Authorize]
  public class CityController : BaseController
  {
    // private readonly Dbclass db;
    private readonly IUnitofWork uow;
    private readonly IMapper mapper;
    public CityController(IUnitofWork uow, IMapper mapper)
    {
      // this.db = db;
      this.uow = uow;
      this.mapper = mapper;
    }
    //api/city
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCities()
    {
      //throw new UnauthorizedAccessException();
      var cities = await uow.cityRepository.GetCitiesAsync();
      var cityDto = mapper.Map<IEnumerable<CityDto>>(cities);
      // var cityDto=from c in cities
      // select new CityDto(){
      //   Id=c.Id,
      //   Name=c.Name
      // };
      return Ok(cityDto);
    }
    // //api/city/add?cityname=Rajmundry
    //  [HttpPost("add")]
    //   [HttpPost("add/{cityname}")]
    // public async Task<IActionResult> AddCity(string cityname)
    // {
    //     City city=new City();
    //    city.Name=cityname;
    //   await db.Cities.AddAsync(city);
    //   await db.SaveChangesAsync();
    //   return Ok(city);
    // }
    //data passed in JSON Format in Body
    [HttpPost("addcity")]
    [AllowAnonymous]
    public async Task<IActionResult> AddCity(CityDto cityDto)
    {
      var city = mapper.Map<City>(cityDto);
      city.LastUpdatedBy= 1;
      city.LastUpdatedOn = DateTime.Now;

      uow.cityRepository.AddCity(city);
      await uow.SaveAsync();
      return StatusCode(200);
    }
    [HttpDelete("remove/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> RemoveCity(int id)
    {

      uow.cityRepository.RemoveCity(id);

      await uow.SaveAsync();
      return Ok(id);
    }
    [HttpPut("update/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
    {
      try
      {
        if (id != cityDto.Id)
        {
          return BadRequest("Update not allowed");
        }
        var cityFromDb = await uow.cityRepository.FindCity(id);
        if (cityFromDb == null)
        {
          return BadRequest("Update not allowed");
        }
        cityFromDb.LastUpdatedBy = 1;
        cityFromDb.LastUpdatedOn = DateTime.Now;
        mapper.Map(cityDto, cityFromDb);
        //throw new Exception("some error occured");
        await uow.SaveAsync();
        return StatusCode(200);
      }
      catch
      {
        return StatusCode(500, "some error occured");
      }

    }
    [HttpPatch("update/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> citytoPatch)
    {
      var cityFromDb = await uow.cityRepository.FindCity(id);
    cityFromDb.LastUpdatedBy = 1;
        cityFromDb.LastUpdatedOn = DateTime.Now;
      citytoPatch.ApplyTo(cityFromDb, ModelState);
      await uow.SaveAsync();
      return StatusCode(200);

    }
  }
}