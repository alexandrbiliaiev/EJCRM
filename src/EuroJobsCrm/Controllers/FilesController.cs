using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace EuroJobsCrm.Controllers
{
    public class FilesController : Controller
    {
        private readonly IHostingEnvironment _env;
        
        public FilesController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        [Route("api/Files/Upload")]
        public string UploadFiles(IFormFile file)
        {
            try
            {
                string filename = ContentDispositionHeaderValue
                               .Parse(file.ContentDisposition)
                               .FileName
                               .Trim('"');

                filename = $"\\files\\{filename}".Replace("\\", "/");

                string filePath = _env.WebRootPath + filename;

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                return filename;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}