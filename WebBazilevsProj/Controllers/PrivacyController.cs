using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebBazilevsProj.Controllers
{
    [Route("Home/Resources")]
    public class ResourcesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}