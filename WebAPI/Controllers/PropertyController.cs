using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Interfaces;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PropertyController:BaseController
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        private readonly IPhotoService PhotoService;

        public PropertyController(IUnitofWork uow,IMapper mapper,IPhotoService photoService)
        {
            this.uow = uow;
            this.mapper = mapper;
            PhotoService = photoService;
        }
        //property/type/1
        [HttpGet("list/{sellrent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellrent){
            var properties=await uow.propertyRepository.GetPropertiesAsync(sellrent);
            var propertylistDto=mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertylistDto);
            // return Ok(properties);
        }
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetails(int id){
            var property=await uow.propertyRepository.GetPropertyDetailsAsync(id);
            var propertyDto=mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDto);
             //return Ok(properties);
        }
         [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDto){
            var property= mapper.Map<Property>(propertyDto);
            int userid=GetUserId();
            property.PostedBy=userid;
            property.LastUpdatedBy=userid;
            uow.propertyRepository.AddProperty(property);
            await uow.SaveAsync();
            return StatusCode(201);

        }
          [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProperty(int id){
        
       
          var userId = GetUserId();
       if(userId==null){
        
                return BadRequest("please login to delete House Property");
            }
             var property = await uow.propertyRepository.GetPropertyByIdAsync(id);            

            if(property.PostedBy != userId)
                return BadRequest("You are not authorised to delete this property");
                else{
                        uow.propertyRepository.DeleteProperty(id);
                         await uow.SaveAsync();
                         return StatusCode(201);
            }

           
            

        }
        [HttpPost("add/photo/{propId}")]
        [AllowAnonymous]
        public async Task<ActionResult<PhotoDto>> AddPropertyPhoto(IFormFile file,int propId){
           var result=await PhotoService.UploadPhotoAsync(file);
           if(result.Error !=null){
            return BadRequest(result.Error.Message);
           }
           var property = await uow.propertyRepository.GetPropertyByIdAsync(propId);
           var photo= new Photo
           {
            imageUrl=result.SecureUrl.AbsoluteUri,
            publicId=result.PublicId
           };
           if(property.Photos.Count==0)
           {
            photo.isPrimary=true;
           }
           property.Photos.Add(photo);
          if(await uow.SaveAsync()) 
          {
            return mapper.Map<PhotoDto>(photo);
          }
           return BadRequest("please try after some time some unknown error occured....");

        }
                //property/set-primary-photo/42/jl0sdfl20sdf2s
        [HttpPost("set-primary-photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> SetPrimaryPhoto(int propId, string photoPublicId)

        {
            var userId = GetUserId();
            var property = await uow.propertyRepository.GetPropertyByIdAsync(propId);            

           if(property.PostedBy != userId)
               return BadRequest("You are not authorised to change the photo");

            if(property == null || property.PostedBy != userId)
                return BadRequest("No such property or photo exists");

            var photo = property.Photos.FirstOrDefault(p => p.publicId == photoPublicId);

            if(photo == null)
                 return BadRequest("No such property or photo exists");

            if(photo.isPrimary) 
                return BadRequest("This is already a primary photo");


            var currentPrimary = property.Photos.FirstOrDefault(p => p.isPrimary);
            if(currentPrimary != null) currentPrimary.isPrimary = false;
            photo.isPrimary = true;

            if (await uow.SaveAsync()) return NoContent();

            return BadRequest("Failed to set primary photo");
        }
        [HttpDelete ("delete-photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> DeletePhoto(int propId, string photoPublicId)
        {
           var userId = GetUserId();
//Console.WriteLine("User ID: " + userId);
       if(userId==null){
                return BadRequest("please login to delete");
            }

            var property = await uow.propertyRepository.GetPropertyByIdAsync(propId);            

            if(property.PostedBy != userId)
                return BadRequest("You are not authorised to delete the photo");

            if(property == null || property.PostedBy != userId)
                return BadRequest("No such property or photo exists");
            
     
            var photo = property.Photos.FirstOrDefault(p => p.publicId == photoPublicId);

            if(photo == null)
                 return BadRequest("No such property or photo exists");

            if(photo.isPrimary) 
                return BadRequest("You can not delete primary photo");

            if (photo.publicId != null)
            {
                var result = await PhotoService.DeletePhotoAsync(photo.publicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }               

            property.Photos.Remove(photo);

            if (await uow.SaveAsync()){return Ok();} 

            return BadRequest("Failed to delete photo");
        }

    }   
}