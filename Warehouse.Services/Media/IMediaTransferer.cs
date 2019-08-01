using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Warehouse.Data.Models;

namespace Warehouse.Services.Media
{
    public interface IMediaTransferer
    {
        bool DirectoryExists(string path);
        bool FileExists(string path);

        void CreateDirectory(string path);
        void DeleteDirectory(string path);

        void RenameFile(string path, string newPath);

        void UploadCompanyLogo(Company company, IFormFile logo);
        Task<bool> UploadProductPhoto(Product product, IFormFile photo);

        void DeleteCompanyMedia(Company company);

        Task<List<string>> GetProductPhotosPaths(Product product);
    }
}
