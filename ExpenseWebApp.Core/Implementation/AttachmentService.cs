using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Dtos;
using ExpenseWebApp.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Implementation
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IWebHostEnvironment _host;

        public AttachmentService(IWebHostEnvironment host)
        {
            _host = host;
        }
        public async Task<string> SaveFilePath(FormDto file)
        {
            if(file.File != null)
            {
                string wwwRootPath = _host.ContentRootPath;
                string fileName = Path.GetFileNameWithoutExtension(file.File.FileName);
                string extension = Path.GetExtension(file.File.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/StaticFiles/Images", fileName);

                using FileStream fileStream = new(path, FileMode.Create);
                await file.File.CopyToAsync(fileStream);
                return path;
            }

            return null;
        }
    }
}
