using FactoryAPI.Models;

namespace FactoryAPI.Utilities
{
    static public class PictureConverter
    {
        static public string SaveImageGetPath(string Base64Image, string name)
        {
            byte[] bytes = Convert.FromBase64String(Base64Image);
            string path = "C:\\Users\\User\\Desktop\\local repos\\FactoryAPI\\pictures\\" + name + ".png";
            System.IO.File.WriteAllBytes(path, bytes);
            return path;
        }

        static public byte[] ReadImage(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
