using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRepo.Data;
using MyRepo.Models;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Collections;
using System.IO.Compression;

namespace MyRepo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ModelDbContext _context;

        private readonly IWebHostEnvironment _env;
        public FileController(ModelDbContext dbContext, IWebHostEnvironment env)
        {
            _context = dbContext;
            _env = env;
        }

        #region FileSystem Endpoints 

        [HttpGet("getallfiles")]
        public async Task<IActionResult> GetAllFiles()
        {
            var files = await _context.UploadedFiles.ToListAsync();
            return Ok(files);
        }

        [HttpGet("getfilebyname")]
        public async Task<IActionResult> GetByFileName(string FileName)
        {
            var files = await _context.UploadedFiles.ToListAsync();
            foreach(var file in files) 
            {
                if(file.FileName.Equals(FileName))
                    return Ok(file);
            }
            return NotFound();
        }

        [HttpPost("Uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            // Read the file into a byte array
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            // Save the file to the database
            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Content = fileBytes
            };
            uploadedFile.Compress();
            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("downloadfile")]
        public async Task<IActionResult> DownloadFile(string FileName)
        {
            var files = await _context.UploadedFiles.ToListAsync();
            foreach (var file in files)
            {
                if(file.FileName.Equals(FileName))
                {
                    file.Decompress();
                    return File(file.Content, file.ContentType, FileName );
                }
            }
            return NotFound();
        }

        [HttpDelete("deletefile")]
        public async Task<IActionResult> DeleteFile(string FileName)
        {
            var files = await _context.UploadedFiles.ToListAsync();
            foreach (var file in files)
            {
                if (file.FileName == FileName)
                {
                    _context.UploadedFiles.Remove(file);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return NotFound();
        }
        #endregion

    }
}
