using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace WebBazilevsProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YupiterController : ControllerBase
    {
        // CREATE IMAGE WITH <img src="/RunNotebook" alt="Generated image"> IN HTML IN PAGE
        public IActionResult RunNotebook()
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "jupyter",
                Arguments = "notebook.ipynb",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = processInfo };
            process.Start();

            var output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                // Handle error
                return StatusCode(500);
            }

            // Parse the output to get the path to the image file
            var imagePath = ParseImagePath(output);

            // Serve the image file to the client
            var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(imageStream, "image/jpeg");
        }

        private string ParseImagePath(string output)
        {
            const string imagePathPrefix = "image_path:";
            var startIndex = output.IndexOf(imagePathPrefix);
            if (startIndex == -1) throw new ArgumentException("Output does not contain image path");
            startIndex += imagePathPrefix.Length;
            var endIndex = output.IndexOf('\n', startIndex);
            if (endIndex == -1) endIndex = output.Length;
            var imagePath = output.Substring(startIndex, endIndex - startIndex).Trim();
            return imagePath;
        }
    }
}
