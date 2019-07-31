using System;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Warehouse.Data.Models;

namespace Warehouse.Services.Media
{
    public class ServerFileSystemTransferer : IMediaTransferer
    {
        private IHostingEnvironment _hostingEnvironment;

        public ServerFileSystemTransferer(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void DeleteDirectory(string path)
        {
            Directory.Delete(path);
        }

        public void RenameFile(string path, string newPath)
        {
            if (!FileExists(path))
            {
                return;
            }

            File.Move(path, newPath);
        }

        public async void UploadCompanyLogo(Company company, IFormFile logo)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images", "companies",
                company.IdentificationCode);

            if (logo.Length > 0)
            {
                if (!DirectoryExists(uploads))
                {
                    CreateDirectory(uploads);
                }

                var fileExtension = logo.FileName.Substring(logo.FileName.LastIndexOf(".", 
                    StringComparison.Ordinal));

                var newFileName = "company_logo" + fileExtension;

                var filePath = Path.Combine(uploads, newFileName);
               
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await logo.CopyToAsync(fileStream);
                }
            }
        }

        public void AddProductPhoto(Product company, IFormFile logo)
        {
            throw new NotImplementedException();
        }
    }
}
