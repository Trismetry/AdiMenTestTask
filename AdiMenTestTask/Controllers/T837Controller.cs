using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdiMenTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class T837Controller : ControllerBase
    {
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Post([FromForm] string FileContent) {
            var parser = new Parse837Form();
            string jsonResult = string.Empty;
            try
            {
                jsonResult= parser.Parse(FileContent);
            }
            catch { 
            //validation impl
            }

                
            if (!string.IsNullOrEmpty(jsonResult))
            {
                return Ok(jsonResult);
            }
            return BadRequest(); ;
        }
             
    }
}
