using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//"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"
namespace WebBazilevsProj.Controllers
{
    [Route("/Home/Chat")]
    public class ChatController : Controller
    {
        private readonly HttpClient _httpClient;

        public ChatController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string message)
        {
            var requestData = new { message };
            var requestBody = JsonSerializer.Serialize(requestData);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/engines/davinci-codex/completions", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            return Content(responseBody, "application/json");
        }
    }
}
