using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Services.Media
{
    public interface IMediaService
    {
        void UploadPhoto(string path, string photoName, byte[] content);
    }
}
