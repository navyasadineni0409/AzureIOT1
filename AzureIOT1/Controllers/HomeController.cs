using Microsoft.AspNetCore.Mvc;

namespace AzureIOT1.Controllers
{
  [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            var path = "wwwroot/index.html";
            StreamReader reader = new StreamReader(path);
            var fileBytes = System.IO.File.ReadAllBytes(path);
            FileContentResult file = File(fileBytes, "text/html");
            return file;
        }
    }
}
