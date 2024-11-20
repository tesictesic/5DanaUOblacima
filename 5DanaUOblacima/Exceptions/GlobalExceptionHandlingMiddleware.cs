using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System;
using FluentValidation;

namespace _5DanaUOblacima.Exceptions
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public GlobalExceptionHandlingMiddleware(RequestDelegate request)
        {
            _requestDelegate = request;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception exception)
            {
                if (exception is ValidationException ex)
                {
                    context.Response.StatusCode = 422;
                    var body = ex.Errors.Select(x => new { Property = x.PropertyName, Error = x.ErrorMessage });

                    await context.Response.WriteAsJsonAsync(body);
                    return;
                }
            }
        }
    }
}
