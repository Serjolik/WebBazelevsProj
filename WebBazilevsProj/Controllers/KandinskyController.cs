using Microsoft.AspNetCore.Mvc;

namespace WebBazilevsProj.Controllers
{
    [Route("Home/Kandinskiy")]
    public class KandinskiyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/generate/{text}")]
        public async Task<IActionResult> GenerateImage(string text)
        {
            return Ok();
            // Create an HTTP client
            var httpClient = new HttpClient();

            // Send a POST request to the Fusion Brain API with the input text
            var response = await httpClient.PostAsync("https://api.fusionbrain.ai/diffusion", new StringContent(text));

            // Read the response content as a byte array
            var imageData = await response.Content.ReadAsByteArrayAsync();

            // Return the image data as a file with content type "image/jpeg"
            return Ok(File(imageData, "image/jpeg"));
        }
    }
}
