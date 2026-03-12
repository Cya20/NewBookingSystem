using BookingWeb.API.BLL;
using BookingWeb.API.BLL.Helpers;
using BookingWeb.API.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace BookingWeb.API.Middleware
{
    public class ApiRequestFlowMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public ApiRequestFlowMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering(); // Enable request body buffering for multiple reads

                using (var reader = new StreamReader(
                    context.Request.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    leaveOpen: true))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    LogRequest(context.Request.Path, context.Request.Method, requestBody);
                    context.Request.Body.Position = 0; // Reset the stream position for further processing
                                                       // Log the request body
                }
                // Pre-processing logic can be added here (e.g., logging, authentication)
                await _next(context); // Call the next middleware in the pipeline
                                      // Post-processing logic can be added here (e.g., response modification, logging)
            }
            catch (CustomException ex)
            {
                await HandleCustomExceptions(context, ex);
            }
            catch (Exception ex) 
            { 
                await HandleCustomExceptions(context, new CustomException(ex.Message));
            }
        }

        private void LogRequest(PathString path, string method, string payLoad)
        {
            if(method == HttpMethods.Get)
            {
                _loggerService.LogInfo($"{method} - Receiving request from {path}");
            }
            else if(method == HttpMethods.Post)
            {
                _loggerService.LogInfo($"{method} - Receiving request from {path} with payload: {payLoad}");
            }
            else if(method == HttpMethods.Put)
            {
                _loggerService.LogInfo($"{method} - Receiving request from {path} with payload: {payLoad}");
            }
            else if(method == HttpMethods.Delete)
            {
                _loggerService.LogInfo($"{method} - Receiving request from {path}");
            }
        }

        private async Task HandleCustomExceptions(HttpContext context, CustomException exception)
        {
            _loggerService.LogError($"Error: {exception.Message}");
            context.Response.ContentType = "application/json";

            var pd = new ProblemDetails
            {
                Title = exception.StatusCode.ToString(),
                Detail = exception.Message,
                Status = (int)exception.StatusCode,
                Instance = context.Request.Path
            };

            if(exception.StatusCode == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(
                    value: pd,
                    options: null,
                    contentType: "application/problem+json"
                    );
                return;

            }

            context.Response.StatusCode = (int)exception.StatusCode;
            await context.Response.WriteAsJsonAsync(
                value: pd,
                options: null,
                contentType: "application/problem+json"
                );
        }
    }
}
