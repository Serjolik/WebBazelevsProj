using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace WebBazilevsProj.Controllers
{
    [ApiController]
    [Route("Home/Anaconda")]
    public class AnacondaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LaunchExe([FromBody] string command)
        {

            string cmdOutput = string.Empty;

            // Create a new process start info for cmd.exe
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            // Start the cmd.exe process
            using (Process cmdProcess = new Process())
            {
                cmdProcess.StartInfo = startInfo;
                cmdProcess.Start();

                // Write the command to the cmd console
                cmdProcess.StandardInput.WriteLine(command);
                cmdProcess.StandardInput.Close();

                // Read the output and error streams
                cmdOutput = cmdProcess.StandardOutput.ReadToEnd();
                string cmdError = cmdProcess.StandardError.ReadToEnd();

                cmdProcess.WaitForExit();
            }

            return Ok(cmdOutput);
        }
    }
}
