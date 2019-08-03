using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
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

            var productPhotosFiles = (await GetProductPhotosFiles(product));

            var photoFile = productPhotosFiles.Where(x =>
            {
                var nameWithoutExt = x.Name.Substring(0, x.Name.IndexOf(x.Extension, StringComparison.Ordinal));
                return (0 == String.CompareOrdinal(nameWithoutExt, photoId.ToString()));
            }).FirstOrDefault();

            if (photoFile != null && photoFile.Exists)
            {
                productPhotosFiles.Remove(photoFile);
                File.Delete(photoFile.FullName);

                //rename all files with ids higher than the deleted one
                DecrementFileNames(productPhotosFiles, photoId);

                return true;
            }

            return false;
        }

        public async Task<List<FileInfo>> GetProductPhotosFiles(Product product)
        {
            List<FileInfo> result = new List<FileInfo>();

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images", "companies",
                product.Company.IdentificationCode, "products", product.Name.Replace(" ", "_").ToLower());

            if (!DirectoryExists(uploads))
            {
                CreateDirectory(uploads);
                return result;
            }

            foreach (var file in Directory.EnumerateFiles(uploads))
            {
                result.Add(new FileInfo(file));
            }

            return result;
        }

        public async Task<List<string>> GetProductPhotosFilesRelative(Product product)
        {
            var files = (await GetProductPhotosFiles(product))
                .Select(f =>
                {
                    var relativePath = f.FullName.Replace("\\", "/");

                    relativePath = relativePath.Substring(relativePath.IndexOf("images",
                        StringComparison.Ordinal));
                    relativePath = "/" + relativePath;

                    return relativePath;
                }).ToList();

            return files;
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

        private void DecrementFileNames(List<FileInfo> files, int fromPhotoId)
        {
            //rename all files with ids higher than the deleted one
            var toRenameId = fromPhotoId + 1;

            while (true)
            {
                var toRenameFile = files.FirstOrDefault(x =>
                {
                    var nameWithoutExt = x.Name.Substring(0, x.Name.IndexOf(x.Extension,
                        StringComparison.Ordinal));

                    return (0 == String.CompareOrdinal(nameWithoutExt,
                                toRenameId.ToString()));
                });

                if (toRenameFile is null)
                {
                    break;
                }

                var newName = Path.Combine(toRenameFile.DirectoryName,
                    (toRenameId - 1).ToString() + toRenameFile.Extension);

                RenameFile(toRenameFile.FullName, newName);

                ++toRenameId;
            }
        }
    }
}
