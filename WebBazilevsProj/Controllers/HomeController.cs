using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenAI_API.Completions;
using OpenAI_API;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using WebBazilevsProj.Models;   

namespace WebBazilevsProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        [Route("Home/Manual")]
        public IActionResult Manual()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string searchText)
        {
            //your OpenAI API key
            string apiKey = "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";
            string answer = string.Empty;
            var openai = new OpenAIAPI(apiKey);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = searchText;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 4000;
            var result = openai.Completions.CreateCompletionAsync(completion);
            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }
            }
            ViewBag.Answer = answer;
            ViewBag.Text = searchText;
            return View();
        }
    }
}