using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace source.utils
{
    public static class HandleFile
    {
        public static List<string> UploadMultipleFile(List<IFormFile> files)
        {
            List<string> result = new List<string>();
            foreach (var formFile in files)
            {
                string path = UploadSingleFile(formFile);
                result.Add(path);
            }

            return result;
        }
        public static string UploadSingleFile(IFormFile? image)
        {
            string ImageName = String.Empty;
            if (image != null)
            {

                //Set Key Name
                ImageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                //Get url To Save
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", ImageName);

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }
            return ImageName;
        }
        public static bool DeleteFile(string filePath)
        {
            string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", filePath);
            try
            {
                System.IO.File.Delete(SavePath);
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}