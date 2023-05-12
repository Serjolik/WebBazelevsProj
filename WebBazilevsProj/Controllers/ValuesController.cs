using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebBazilevsProj.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        [Route("myaction")]
        public IActionResult MyAction([FromBody] string inputString)
        {
            // process the input string as needed
            string outputString = "Processed input: " + inputString;

            // create response object
            var response = new
            {
                message = outputString
            };

            return Ok(response); // send JSON response with 200 status code
        }
    }

}
