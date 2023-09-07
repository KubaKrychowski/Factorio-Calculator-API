using System.Net;
using Microsoft.AspNetCore.Http;
using Infrastructure.Exceptions;
using System.Text.Json;
using System.Text;

namespace Infrastructure.GlobalExceptionHandler
{
    public class GlobalExceptionHandler : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (MissingParamException ex)
            {
                var jsonObject = JsonSerializer.Serialize(ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
            }
            catch (NotFoundException ex)
            {
                var jsonObject = JsonSerializer.Serialize(ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                var jsonObject = JsonSerializer.Serialize(
                    string.Format(Resources.Exceptions.Exceptions.UnindentifiedException, ex.Message));
                
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
            }
        }
    }
}
