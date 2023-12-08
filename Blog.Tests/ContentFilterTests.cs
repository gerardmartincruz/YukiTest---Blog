using Blog.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Net;

namespace Blog.Tests
{
    [TestFixture]
    public class ContentFilterTests
    {
        [Test]
        public async Task InvokeAsync_WithJsonAcceptHeader_SetsContentTypeToJson()
        {
            // Arrange
            var logger = Substitute.For<ILogger<ContentFilter>>();
            var filter = new ContentFilter(logger);

            var context = new DefaultHttpContext();
            context.Request.Headers["Accept"] = "application/json";
            var invocationContext = Substitute.For<EndpointFilterInvocationContext>();
            invocationContext.HttpContext.Returns(context);

            // Act
            await filter.InvokeAsync(invocationContext, async _ => null);

            // Assert
            Assert.That("application/json", Is.EqualTo(context.Response.ContentType));
        }
        
        [Test]
        public async Task InvokeAsync_WithXmlAcceptHeader_SetsContentTypeToXml()
        {
            // Arrange
            var logger = Substitute.For<ILogger<ContentFilter>>();
            var filter = new ContentFilter(logger);

            var context = new DefaultHttpContext();
            context.Request.Headers["Accept"] = "application/xml";
            var invocationContext = Substitute.For<EndpointFilterInvocationContext>();
            invocationContext.HttpContext.Returns(context);

            // Act
            await filter.InvokeAsync(invocationContext, async _ => null);

            // Assert
            Assert.That("application/xml", Is.EqualTo(context.Response.ContentType));
        }

        [Test]
        public async Task InvokeAsync_WithAnotherAcceptHeader_SetsUnsuported()
        {
            // Arrange
            var logger = Substitute.For<ILogger<ContentFilter>>();
            var filter = new ContentFilter(logger);

            var context = new DefaultHttpContext();
            context.Request.Headers["Accept"] = "anythingelse";
            var invocationContext = Substitute.For<EndpointFilterInvocationContext>();
            invocationContext.HttpContext.Returns(context);

            // Act
            await filter.InvokeAsync(invocationContext, async _ => null);
            
            // Assert
            Assert.That((int)HttpStatusCode.UnsupportedMediaType, Is.EqualTo(context.Response.StatusCode));
        }
    }
}
