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
        public DocumentFilesDto UploadFiles(IFormFile file, string name, string description, int ownerId, string ownerType)
        {
            try
            {
                if (file == null)
                {
                    return new DocumentFilesDto
                    {
                        Success = false,
                        ErrorMessage = "File is not selected"
                    };
                }

                string fileName = SaveFile(file);

                using (var context = new DB_A12601_bielkaContext())
                {
                    DocumentFiles fileEntity = new DocumentFiles
                    {
                        DcfUrl = fileName,
                        DcfAuditCd = DateTime.UtcNow,
                        DcfAuditCu = User.GetUserId(),
                        DcfAuditMd = DateTime.UtcNow,
                        DcfAuditMu = User.GetUserId(),
                        DcfName = name ?? string.Empty,
                        DcfDescription = description ?? string.Empty,
                    };

                    switch (ownerType)
                    {
                        case "contragent":
                            fileEntity.DcfCntId = ownerId;
                            break;
                        case "client":
                            fileEntity.DcfCliId = ownerId;
                            break;
                        case "offer":
                            fileEntity.DcfOfrId = ownerId;
                            break;
                    }

                    context.Add(fileEntity);
                    context.SaveChanges();
                    return new DocumentFilesDto(fileEntity);
                } 
            }
            catch (Exception ex)
            {
                return new DocumentFilesDto
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private string SaveFile(IFormFile file)
        {
            string filename = ContentDispositionHeaderValue
                .Parse(file.ContentDisposition)
                .FileName
                .Trim('"');

            filename = $"\\files\\{Guid.NewGuid()}\\{filename}".Replace("\\", "/");

            string filePath = _env.WebRootPath + filename;
            string path = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return filename;
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
        public async Task<bool> DeleteFile([FromBody] int fileId)
        {

            using (var context = new DB_A12601_bielkaContext())
            {
                DocumentFiles fileEntity = context.DocumentFiles.FirstOrDefault(d => d.DcfId == fileId);


                if (fileEntity == null)
                {
                    return true;
                }

                string filePath = _env.WebRootPath + fileEntity.DcfUrl;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }


                context.DocumentFiles.Remove(fileEntity);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}