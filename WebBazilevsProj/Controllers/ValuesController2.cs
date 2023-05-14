using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using Python.Included;
using Python.Runtime;

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

            string fileName = @"C:\TestNeuro\Test.py";
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\TestNeuro\venv\Scripts\python.exe", fileName);
            p.StartInfo.WorkingDirectory = "C:\\TestNeuro";
            p.Start();
            p.WaitForExit();
            p.Close();

            string imagePathFile = "C:\\TestNeuro\\image.jpg";
            if (imagePathFile != null)
            {
                Console.WriteLine(imagePathFile);
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePathFile);
                return new FileContentResult(imageBytes, "image/jpeg");
            }

            return View();
        }
    }
}
