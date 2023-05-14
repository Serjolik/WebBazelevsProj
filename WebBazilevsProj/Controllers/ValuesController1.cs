using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WebBazilevsProj.Controllers
{
    [Route("ChatOpenAI")]
    public class ChatOpenAIController : Controller
    {
        private readonly HttpClient _httpClient;

        public ChatOpenAIController(HttpClient httpClient)
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
            // Replace YOUR_API_KEY with your actual OpenAI API key
            var apiKey = "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";

            // Create the request data
            var requestData = new { prompt = message, temperature = 0.5, max_tokens = 50 };

            // Convert the request data to a JSON string
            var requestDataJson = JsonConvert.SerializeObject(requestData);

            // Create the request content
            var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");

            // Set the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", apiKey);

            // Make the request to the OpenAI API
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/engines/davinci-codex/completions", requestContent);

            // Get the response content as a string
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize the response content to a dynamic object
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

            // Get the text result from the response object
            var result = responseObject.choices[0].text;

            // Pass the result to the view
            ViewBag.Result = result;

            return View();
        }
    }
}
