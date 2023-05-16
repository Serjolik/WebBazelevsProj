using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API; 

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
            var oldText = searchText;

            string editedText = formating(searchText);
            //your OpenAI API key 
            string apiKey = "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";
            string answer = string.Empty;
            var openai = new OpenAIAPI(apiKey);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = editedText;
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
            ViewBag.Text = oldText;

            return View();
        }

        private string formating(string text)
        {
            string newText = "Write a story about "
                + text
                + " . 10 offers required.";
            return newText;
        }
    }
}