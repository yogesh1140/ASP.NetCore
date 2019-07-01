using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net.Http.Headers;

namespace Angular5FileUpload.Controllers
{
    [Route("api/[Controller]")]
    public class UploadController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<UploadController> _logger;

        public UploadController(IHostingEnvironment hostingEnvironment,
         ILogger<UploadController> logger
        )
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                string fullPath =string.Empty;
                 string fileName= string.Empty;
                if (file.Length > 0)
                {
                   fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                   //fileName ="temp"+ fileName.Substring(fileName.LastIndexOf('.'));
                    fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok(new {Message ="Uploaded Successfully", filePath= $"/upload/{fileName}"});
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to upload File to server: {ex}");
                return BadRequest("Failed to upload File to server");
            }
        }
    }
}