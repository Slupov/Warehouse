using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Warehouse.Services.Media
{
    public class ServerMediaService : IMediaService
    {
        public void UploadPhoto(string path, string photoName, byte[] content)
        {
            if (!Directory.Exists(path))
            {
                // create directory if it does not exist
                Directory.CreateDirectory(path);
            }

            MemoryStream ms = new MemoryStream(content);
            Image img = Image.FromStream(ms);
            img.Save(path + photoName, ImageFormat.Jpeg);
        }
    }
}
