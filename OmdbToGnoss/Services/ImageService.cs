using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Services
{
    public class ImageService
    {
        public static byte[] DownloadImage(string imageUrl)
        {
            byte[] bytes = null;
            Uri imageUri;
            if (Uri.TryCreate(imageUrl, UriKind.Absolute, out imageUri))
            {
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", imageUri.AbsolutePath);

                if (File.Exists(imagePath))
                {
                    bytes = File.ReadAllBytes(imagePath);
                }
                else
                {
                    WebClient webClient = new WebClient();

                    try
                    {
                        bytes = webClient.DownloadData(imageUri);

                        if (imagePath.Contains("?"))
                        {
                            imagePath = imagePath.Substring(0, imagePath.IndexOf('?'));
                        }

                        FileInfo info = new FileInfo(imagePath);

                        if (!Directory.Exists(info.Directory.FullName))
                        {
                            Directory.CreateDirectory(info.Directory.FullName);
                        }

                        File.WriteAllBytes(imagePath, bytes);
                    }
                    catch (Exception ex)
                    {
                        Gnoss.ApiWrapper.Helpers.LogHelper.Instance.Error($"{ex.Message} Stack: {ex.StackTrace}");
                    }
                }
            }

            return bytes;
        }

        public static string GetImageExtensionFromURL(string imageUrl)
        {
            string extension = "jpg";
            int lastDotIndex = imageUrl.LastIndexOf('.');
            if(lastDotIndex > 0)
            {
                extension = imageUrl.Substring(lastDotIndex + 1);
                if (extension.Contains('?'))
                {
                    extension = extension.Remove(extension.IndexOf('?'));
                }
                if (extension.Contains('#'))
                {
                    extension = extension.Remove(extension.IndexOf('#'));
                }
            }

            return extension;
        }
    }
}
