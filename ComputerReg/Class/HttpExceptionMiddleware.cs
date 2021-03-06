using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

internal class HttpExceptionMiddleware
{
    private readonly RequestDelegate next;

    public HttpExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.next.Invoke(context);
        }
        catch (HttpException httpException)
        {
            context.Response.StatusCode = httpException.StatusCode;
            var responseFeature = context.Features.Get<Microsoft.AspNetCore.Http.Features.IHttpResponseFeature>();
            responseFeature.ReasonPhrase = httpException.Message;
        }
    }
}