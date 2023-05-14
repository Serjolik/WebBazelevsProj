using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace WebBazilevsProj.Controllers
{
    [Route("Home/search")]
    public class GoogleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string text)
        {
            string imagePathFile = "C:\\TestNeuro\\image.jpg";
            if (imagePathFile != null)
            {
                Console.WriteLine(imagePathFile);
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePathFile);
                return new FileContentResult(imageBytes, "image/jpeg");
            }

            // Run the Python script
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";
            start.Arguments = string.Format("{0} {1}", "C:\\TestNeuro\\Test.py", text);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    string imagePath = System.IO.File.ReadAllText(imagePathFile).Trim();
                    ViewBag.ImagePath = imagePath;
                }
            }

            return View();
        }
    }
}
