using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebKnopka.Models;

namespace WebKnopka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm]UploadProductImageViewModel model)
        {
            string fileName = string.Empty;
            if(model.Image!=null)
            {
                var fileExp = Path.GetExtension(model.Image.FileName).Trim();
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                fileName = Path.GetRandomFileName() + fileExp;
                using(var stream = System.IO.File.Create(Path.Combine(dir, fileName)))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }
            return Ok();
        }
    }
}
