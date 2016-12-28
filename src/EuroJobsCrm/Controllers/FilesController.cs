using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
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

        [HttpPost]
        [Route("api/Files/Save")]
        public async Task<DocumentFilesDto> SaveFile(DocumentFilesDto file)
        {
            using (var context = new DB_A12601_bielkaContext())
            {
                DocumentFiles fileEntity = null;
                if (file.Id != 0)
                {
                    fileEntity = context.DocumentFiles.FirstOrDefault(d => d.DcfId == file.Id);
                }

                if (fileEntity == null)
                {
                    fileEntity = new DocumentFiles
                    {
                        DcfAuditCd = DateTime.UtcNow,
                        DcfAuditCu = User.GetUserId()
                    };
                    context.DocumentFiles.Add(fileEntity);
                }

                fileEntity.DcfIdcId = file.IdtDocumentId;
                fileEntity.DcfAuditMd = DateTime.UtcNow;
                fileEntity.DcfAuditMu = User.GetUserId();
                fileEntity.DcfCliId = file.ClientId;
                fileEntity.DcfCntId = file.ContragentId;
                fileEntity.DcfDescription = file.Description;
                fileEntity.DcfInvId = file.InvoiceId;
                fileEntity.DcfName = file.Name;
                fileEntity.DcfOfrId = file.OfferId;

                await context.SaveChangesAsync();
                file.Id = fileEntity.DcfId;
                return file;
            }
        }

        [HttpPost]
        [Route("api/Files/Delete")]
        public async void DeleteFile(int fileId)
        {

            using (var context = new DB_A12601_bielkaContext())
            {
                DocumentFiles fileEntity = context.DocumentFiles.FirstOrDefault(d => d.DcfId == fileId);


                if (fileEntity == null)
                {
                    return;
                }

                context.DocumentFiles.Remove(fileEntity);
                await context.SaveChangesAsync();

            }
        }
    }
}