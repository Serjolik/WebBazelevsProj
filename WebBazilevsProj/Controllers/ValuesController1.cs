using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebBazilevsProj.Controllers
{
    [Route("ImageCreate")]
    [ApiController]
    public class ValuesController1 : ControllerBase
    {
        [HttpPost]
        public IActionResult GenerateImage(string inputString)
        {
            string pythonScriptPath = @"C:\TestNeuro\Test.py"; // path to your Python script
            string pythonExePath = @"C:\Program Files\Python311\python.exe"; // path to Python interpreter
        
            // Call the Python script to generate the image
            string arguments = $"{pythonScriptPath} {inputString}";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = pythonExePath;
            startInfo.Arguments = arguments;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            string output = string.Empty;
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output += args.Data;
                    }
                };
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
            }

            // Do something with the output of the Python script
            // In this example, we just return the output as a string
            return Ok(output);
        }

    }
}
