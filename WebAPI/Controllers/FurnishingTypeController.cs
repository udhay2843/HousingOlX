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
    public class FurnishingTypeController:BaseController
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;
        

        public FurnishingTypeController(IUnitofWork uow,IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFurnishingType(){
            var furnishtype= await uow.furnishingTypeRepository.GetFurnishingTypesAsync();
               var furnishTypeDto=mapper.Map<IEnumerable<KeyValuePairDto>>(furnishtype);
            return Ok(furnishTypeDto);
        }
    }
}