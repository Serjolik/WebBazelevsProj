using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using WebBazilevsProj.Models;  

namespace WebBazilevsProj.Controllers
{
    [Route("Search")]
    public class GoogleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Browse")]
        public async Task<IActionResult> Search(string query)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://www.google.com/search?q={query}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            ViewBag.ResultText = responseBody;

            return View("Index");
        }
    }
}
