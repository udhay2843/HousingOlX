using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController:ControllerBase
    {
        protected int GetUserId(){
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
          
        }
    }
}