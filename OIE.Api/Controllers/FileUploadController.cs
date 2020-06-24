using ApplicationCore;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Transforms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
namespace OIE.Api.Controllers
{
    /// <summary>
    /// Resources Web API controller.
    /// </summary>
    [Route("api/[controller]")]
    // Authorization policy for this API.
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
    public class FileUploadController : Controller
    {

        private IList<string> DOCUMENT_FILE_TYPE = new string[] { "doc", "xls", "mpp", "pdf", "ppt", "tiff", "bmp", "docx", "xlsx", "pptx", "ps", "odt", "ods", "odp", "odg" };

        private IList<string> IMAGE_FILE_TYPE = new string[] { "png", "gif", "jpg", "jpeg" };
        private IList<string> VIDEO_FILE_TYPE = new string[] { "mp4", "webm", "ogg" };


        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string type)
        {
            if (file == null) return BadRequest("File is null");
            if (file.Length == 0) return BadRequest("File is empty");

            try
            {
                string extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1].ToLower();
                string fileName = file.FileName.Replace("." + extension, DateTime.Now.ToString("ddMMyyhhmmss.") + extension);

                fileName = CustomFileName(type);

                string filePath = "MediaFiles/" + fileName;
                if (!System.IO.Directory.Exists("MediaFiles"))
                    System.IO.Directory.CreateDirectory("MediaFiles");

                string fileUrl = "/" + fileName;

                // Delete an existing file.
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                using (Stream stream = file.OpenReadStream())
                {
                    using (var fileStream = System.IO.File.Create(filePath))
                    {
                        stream.CopyTo(fileStream);
                    }
                }

                try
                {
                    using (Image<Rgba32> image = Image.Load(filePath))
                    {
                        int ratio = image.Width / 500;
                        image.Mutate(x => x.Resize(image.Width / ratio, image.Height / ratio));
                        image.Save(filePath);
                    }
                }
                catch { }

                FileType fileType = IMAGE_FILE_TYPE.Contains(extension) ? FileType.Image : FileType.Document;
                fileType = VIDEO_FILE_TYPE.Contains(extension) ? FileType.Video : fileType;

                return Ok(new FileInfo() { FileUrl = fileUrl, FileName = file.FileName, FileType = fileType });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CustomFileName(string type)
        {
            if (type == "document1")
            {
                return "document1.pdf";
            }
            else if (type == "document2")
            {
                return "document2.pdf";
            }

            return type;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum FileType
    {
        [EnumMember(Value = "document")]
        Document = 0x1,
        [EnumMember(Value = "image")]
        Image = 0x2,
        [EnumMember(Value = "video")]
        Video = 0x3
    }

    public class FileInfo
    {
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
    }
}
