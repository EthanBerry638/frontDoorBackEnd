using firstDoorBackEnd.Exceptions;

namespace firstDoorBackEnd.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (CareerJetBadRequestException ex)
            {
                await HandleCareerJetBadRequestException(context, ex);
            }
            catch (CareerJetForbiddenException ex)
            {
                await HandleCareerJetForbiddenException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericException(context, ex);
            }
        }

        private async Task HandleCareerJetBadRequestException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(ex.Message);
        }

        private async Task HandleCareerJetForbiddenException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(ex.Message);
        }

        private async Task HandleGenericException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = "firstDoor encountered an unexpected error :(" });
        }
    }
}
