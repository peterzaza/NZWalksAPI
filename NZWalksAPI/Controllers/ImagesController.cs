using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {

            }

            return BadRequest(ModelState);
        }
        private void ValidateFileUpload (ImageUploadRequestDto requset)
        {
            var allowedExtenstion = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtenstion.Contains(Path.GetExtension(requset.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (requset.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, Please upload a smaller size file.");
            }
        }
    }
}
