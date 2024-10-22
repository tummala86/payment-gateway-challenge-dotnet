using System.Net;
using FluentAssertions;
using PaymentGateway.Api.Constants;
using PaymentGateway.Test.Integration.Fixtures;
using Xunit;

namespace PaymentGateway.Test.Integration
{
    public class HealthCheckTest : TestServerFixture
    {
        [Fact]
        public async Task TestHealthCheck()
        {
            // Arrange
            var httpClient = Server.CreateClient();

            // Act
            var response = await httpClient.GetAsync(ApiRoutes.HealthChecks.Internal);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}