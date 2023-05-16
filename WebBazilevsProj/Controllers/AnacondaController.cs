using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Python.Runtime;
using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

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
            var input = command.Split("+")[0];
            var number = command.Split("+")[1];
            string cmdOutput = string.Empty;

            // Create a new process start info for cmd.exe
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = "stable-diffusion-main";
            startInfo.Arguments = "/c \"conda activate ldm && python scripts/txt2img.py --prompt \"" + command + "\" --H 512 --W 512 --seed 40 --n_iter " + number + " --ddim_steps 50";

            Process.Start(startInfo);

            int num = int.Parse(input);
            return Ok("Waiting " + num % 10 + 1 + "m");
        }
    }
}
