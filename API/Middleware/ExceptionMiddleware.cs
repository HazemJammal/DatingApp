using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly IHostEnvironment env;
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware>  logger, IHostEnvironment env )
        {
            this.next = next;
            this.env = env;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext){
            try{
                await next(httpContext);
            }
            catch(Exception ex){
                logger.LogError(ex,ex.Message);
                httpContext.Response.ContentType  ="application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var respone = env.IsDevelopment() ? 
                new ApiException(httpContext.Response.StatusCode, ex.Message,ex.StackTrace?.ToString())
                :new ApiException(httpContext.Response.StatusCode, ex.Message,"Internal Server Error");


                var options = new JsonSerializerOptions{PropertyNamingPolicy= JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(respone, options);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}