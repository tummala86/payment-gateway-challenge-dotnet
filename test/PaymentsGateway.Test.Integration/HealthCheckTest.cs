using FluentAssertions;
using PaymentsGateway.Api.Constants;
using PaymentsGateway.Test.Integration.Fixtures;
using System.Net;
using Xunit;

namespace PaymentsGateway.Test.Integration
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
