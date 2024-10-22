using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using PaymentGateway.Api;

namespace PaymentGateway.Test.Integration.Fixtures
{
    public class TestServerFixture : WebApplicationFactory<Program>, IDisposable
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }

        public new void Dispose()
        {
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}