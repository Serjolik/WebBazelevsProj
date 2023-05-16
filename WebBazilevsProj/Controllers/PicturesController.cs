using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace WebBazilevsProj.Controllers
{
    public class PicturesController : Controller
    {
        [Route("Home/Pictures")]
        public IActionResult DownloadImages()
        {
            var imageFolder = "stable-diffusion-main\\outputs\\txt2img-samples\\samples";
            var imageFiles = Directory.GetFiles(imageFolder, "*.png").OrderByDescending(f => f).Take(5);

            var fileBytes = new List<byte[]>();
            var fileNames = new List<string>();

            foreach (var file in imageFiles)
            {
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);

                    fileBytes.Add(buffer);
                    fileNames.Add(Path.GetFileName(file));
                }
            }

            var zipFileBytes = CreateZipFile(fileBytes, fileNames);

            return File(zipFileBytes, "application/zip", "downloaded_images.zip");
        }

        private byte[] CreateZipFile(IEnumerable<byte[]> fileBytes, IEnumerable<string> fileNames)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    for (var i = 0; i < fileBytes.Count(); i++)
                    {
                        var fileEntry = zipArchive.CreateEntry(fileNames.ElementAt(i));

                        using (var entryStream = fileEntry.Open())
                        {
                            entryStream.Write(fileBytes.ElementAt(i), 0, fileBytes.ElementAt(i).Length);
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }
    }
}
