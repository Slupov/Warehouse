using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Warehouse.Data.Models;

namespace Warehouse.Services.Media
{
    public class ServerFileSystemTransferer : IMediaTransferer
    {
        private IHostingEnvironment _hostingEnvironment;
        private IGenericDataService<Product> _products;

        public ServerFileSystemTransferer(IHostingEnvironment env,
            IGenericDataService<Product> products)
        {
            _hostingEnvironment = env;
            _products           = products;
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

        public async void DeleteCompanyMedia(Company company)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images", "companies",
                company.IdentificationCode);

            if (DirectoryExists(uploads))
            {
                DeleteDirectory(uploads);
            }
        }

        public async Task<bool> DeleteProductPhoto(int productId, int photoId)
        {
            var product = await _products.GetSingleOrDefaultAsync(x => x.Id == productId);

            if (product is null)
            {
                return false;
            }

            var uploads = GetProductImagesPath(product);

            if (!DirectoryExists(uploads))
            {
                return false;
            }


            // find file in product uploads directory
            var file = Directory.GetFiles(uploads)
                .Where(path =>
                {
                    var directoryString      = "\\";
                    var indexOfLastDirectory = path.LastIndexOf(directoryString, StringComparison.Ordinal);
                    var indexOfLastDot       = path.LastIndexOf(".", StringComparison.Ordinal);

                    var photoIdStr     = photoId.ToString();
                    var currPhotoIdStr = path.Substring(indexOfLastDirectory + directoryString.Length,
                                                        indexOfLastDot - indexOfLastDirectory - 1);
                    
                    return (0 == String.CompareOrdinal(photoIdStr, currPhotoIdStr));
                })
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(file))
            {
                File.Delete(file);
                return true;
            }

            return false;
        }

        public async Task<List<string>> GetProductPhotosPaths(Product product)
        {
            List<string> result = new List<string>();

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images", "companies",
                product.Company.IdentificationCode, "products", product.Name.Replace(" ", "_").ToLower());

            if (!DirectoryExists(uploads))
            {
                CreateDirectory(uploads);
                return result;
            }

            var wwwrootDir = "wwwroot/";
            string relativeFile = string.Empty;

            foreach (var file in Directory.EnumerateFiles(uploads))
            {
                //turn current absolute file path to a relative one
                relativeFile = file.Replace("\\", "/");

                var wwwrootIdx = relativeFile.IndexOf(wwwrootDir);

                relativeFile = "/" + relativeFile.Substring(wwwrootIdx + wwwrootDir.Length);

                result.Add(relativeFile);
            }

            return result;
        }

        public async Task<bool> UploadProductPhoto(Product product, IFormFile photo)
        {
            var uploads = GetProductImagesPath(product);

            if (!DirectoryExists(uploads))
            {
                CreateDirectory(uploads);
            }

            var newPhotoId = Directory.EnumerateFiles(uploads).Count() + 1;
            var fileExtension = photo.FileName.Substring(photo.FileName.LastIndexOf(".",
                StringComparison.Ordinal));

            var newFileName = newPhotoId.ToString() + fileExtension;

            var filePath = Path.Combine(uploads, newFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
                return true;
            }
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

        private string GetProductImagesPath(Product product)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images", "companies",
                product.Company.IdentificationCode, "products", product.Name.Replace(" ", "_").ToLower());

            return uploads;
        }
    }
}
