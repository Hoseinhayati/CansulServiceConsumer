using Microsoft.AspNetCore.Mvc;

namespace ServiceConsumer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpClient = _httpClientFactory.CreateClient("ServiceProvider");
            var serviceProviderUrl = "https://localhost:7171"; // Replace with the actual Service Provider URL

            var url = $"{serviceProviderUrl}/api/values";
            var response = await httpClient.GetAsync(url);
            //var response = await httpClient.GetAsync("http://localhost:8500/api/values");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }

            return BadRequest("Error occurred while consuming the service.");
        }
    }
}
