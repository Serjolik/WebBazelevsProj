using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace WebBazilevsProj.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatClient _chatClient;

        public ChatController(ChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            var response = await _chatClient.SendMessageAsync(message);
            return Ok(response);
        }
    }

    public class ChatClient
    {
        private readonly HttpClient _httpClient;
        private const string ChatEndpoint = "https://api.openai.com/v1/engines/davinci-codex/completions";

        public ChatClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, ChatEndpoint);
            var requestBody = new
            {
                prompt = message,
                max_tokens = 1024,
                temperature = 0.7,
                n = 1,
                stop = "\n"
            };
            var json = JsonSerializer.Serialize(requestBody);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<dynamic>(responseJson);

            return responseObject.choices[0].text;
        }
    }
}
