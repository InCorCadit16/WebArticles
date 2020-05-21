using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using WebArticles.WebAPI.Infrastructure.Exceptions;

namespace WebArticles.WebAPI.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch (EntityNotFoundException enf)
            {
                _logger.LogError(enf, "EntityNotFoundException occured");
                await ReturnResponseAsync(context, enf.PublicMessage, enf.ReturnStatusCode);
            } catch (FormInvalidException fi)
            {
                _logger.LogError(fi, "FormInvalidException occured");
                await ReturnResponseAsync(context, fi.PublicMessage, fi.ReturnStatusCode);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured");
                await ReturnResponseAsync(context, "Internal Server Error. Please contact the development team");
            }
        }

        private async static Task ReturnResponseAsync(HttpContext context, string message, int statusCode = StatusCodes.Status500InternalServerError)
        {
            context.Response.ContentType = "applic  ation/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(message);
        }
    }
}
