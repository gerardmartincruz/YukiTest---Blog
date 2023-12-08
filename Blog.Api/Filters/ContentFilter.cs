using Blog.Entities;
using Blog.Models;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System.Xml.Serialization;

namespace Blog.Filters
{
    public class ContentFilter : IEndpointFilter
    {
        private readonly ILogger _logger;

        public ContentFilter(ILogger<ContentFilter> logger)
        {
            _logger = logger;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            try
            {
                var postResult = await next(context);
                if (context.HttpContext.Request.Headers.Accept == "application/json")
                {
                    context.HttpContext.Response.ContentType = "application/json";

                    return postResult;
                }
                else if (context.HttpContext.Request.Headers.Accept == "application/xml")
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PostModel));
                    context.HttpContext.Response.ContentType = "application/xml";
                    var allowSynchronousIoOption = context.HttpContext.Features.Get<IHttpBodyControlFeature>();
                    if (allowSynchronousIoOption != null)
                    {
                        allowSynchronousIoOption.AllowSynchronousIO = true;
                    }
                    if (postResult is not null)
                    {
                        serializer.Serialize(context.HttpContext.Response.Body, postResult);
                    }
                    return postResult;
                }
                else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    return "Content not supported";
                }
            }
            catch (InvalidOperationException  e)
            {
                _logger.LogError(e.Message, e);
                return "Oops the post you're searching for is not available";
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return "Oops something went wrong";
            }

            
        }
    }
}
