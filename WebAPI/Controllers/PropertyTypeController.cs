using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Interfaces;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    public class PropertyTypeController:BaseController
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public PropertyTypeController(IUnitofWork uow,IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
         [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyType(){
            var propertyTypes=await uow.propertyTypeRepository.GetPropertyTypeAsync();
            var propertyTypeDto=mapper.Map<IEnumerable<KeyValuePairDto>>(propertyTypes);
            return Ok(propertyTypeDto);

        }
    }
}