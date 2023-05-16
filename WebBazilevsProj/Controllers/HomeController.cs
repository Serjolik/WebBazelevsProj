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
        public IActionResult Index(string searchText, string numbersOfOffers, string textStyle)
        {
            var oldText = searchText;

            string editedText = formating(searchText, numbersOfOffers, textStyle);
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
            ViewBag.Number = numbersOfOffers;
            ViewBag.TextStyle = textStyle;

            return View();
        }

        private string formating(string text, string numbersOfOffers, string textStyle)
        {
            string newText = "Write a story about " + text;

            if (numbersOfOffers != null)
                newText += ". Need text only in" + numbersOfOffers + " sentence.";

            if (textStyle != null)
                newText += " use text style: " + textStyle;

            newText += ". Text must be in present tense";

            return newText;
        }
    }
}