using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI.Extensions
{
    public static class ExceptionsMiddleware
    {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env){
           if (env.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else{
    app.UseExceptionHandler(
        options=>{options.Run(
            async context =>{
                context.Response.StatusCode=500;
                var ex=context.Features.Get<IExceptionHandlerFeature>();
                if(ex !=null){
                    context.Response.WriteAsync(ex.Error.Message);
                }
            }
        );
        }
    );
}

    }
    }
}