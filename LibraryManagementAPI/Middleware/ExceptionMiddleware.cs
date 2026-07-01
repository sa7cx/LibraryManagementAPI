using Application;
using Application.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException ex)
        {
            await HandleApiException(context, ex);
        }
        catch (Exception ex)
        {
            await HandleUnknownException(context, ex);
        }
    }

    private static async Task HandleApiException(HttpContext context, ApiException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex.StatusCode;
        var res = ApiResponse.Fail(ex.Message, ex.StatusCode);
        await context.Response.WriteAsJsonAsync(res);
    }

    private static async Task HandleUnknownException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        var res = ApiResponse.Fail(ex.Message,500);
        await context.Response.WriteAsJsonAsync(res);
    }
}