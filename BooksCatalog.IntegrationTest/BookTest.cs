using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace BooksCatalog.IntegrationTest
{
    public class BookTest
    {
        private readonly HttpClient _client;

        public BookTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>()
            );
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task BookGetAllTestAsync(string method)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "api/books/");
            //Act
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET", 1)]
        public async Task BookGetTestAsync(string method, int? id)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"api/books/{id}");
            //Act
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
