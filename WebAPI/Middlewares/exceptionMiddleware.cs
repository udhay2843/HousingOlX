using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Errors;

namespace WebAPI.Middlewares
{
    public class exceptionMiddleware
    {
        private readonly RequestDelegate next;
         private readonly ILogger logger;
        private readonly IHostEnvironment env;
        public exceptionMiddleware(RequestDelegate next,ILogger<exceptionMiddleware>logger, IHostEnvironment env)
        {
            this.env = env;
            this.next=next;
            this.logger=logger;
        }
        public async Task Invoke(HttpContext context){
            try{
                 await next(context);
            }
            catch(Exception ex){
                ApiError response;
                HttpStatusCode StatusCode=HttpStatusCode.InternalServerError;
                string message;
                var exceptiontype=ex.GetType();
                if(exceptiontype==typeof(UnauthorizedAccessException)){
                    StatusCode=HttpStatusCode.Unauthorized;
                    message="you are not authorised";
                }
                else{
                     StatusCode=HttpStatusCode.InternalServerError;
                    message="some unknown error occured";
                }
                if(env.IsDevelopment()){
                    response=new ApiError((int)StatusCode,message,ex.StackTrace.ToString());
                }
                else{
                     response=new ApiError((int)StatusCode,message);
                }
                logger.LogError(ex,ex.Message);
                context.Response.StatusCode=500;
                context.Response.ContentType="application/json";
               await context.Response.WriteAsync(response.ToString());
            }
           

        }
    }
}