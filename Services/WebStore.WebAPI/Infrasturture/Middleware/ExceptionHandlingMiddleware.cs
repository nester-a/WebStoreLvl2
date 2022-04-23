﻿namespace WebStore.WebAPI.Infrasturture.Middleware;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {

            await _next(context);
        }
        catch (Exception ex)
        {
            HandleException(context, ex);
            throw;
        }
    }

    private void HandleException(HttpContext context, Exception error)
    {
        _logger.LogError(error, "Ошибка при обработке запроса {0}", context.Request.Path);
    }
}
