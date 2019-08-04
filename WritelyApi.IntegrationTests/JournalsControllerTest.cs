using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WritelyApi;
using WritelyApi.Journals;
using Xunit;

namespace WritelyApi.IntegrationTests
{
    public class JournalsControllerTest : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebAppFactory<Startup> _factory;

        public JournalsControllerTest(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            _client.BaseAddress = new System.Uri("https://localhost:5001/");
        }

        [Fact]
        public async Task CanFindSingleJournal()
        {
            // UserID - TestUser12345
            var httpResponse = await _client.GetAsync("api/journals");

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}