using FactoryAPI.Models;
using System.Linq;

namespace FactoryAPI.Utilities
{
    static public class PictureConverter
    {
        static public string SaveImageGetPath(string Base64Image, string name)
        {
            byte[] bytes = Convert.FromBase64String(Base64Image);
            string path = UpdatePath(name);
            System.IO.File.WriteAllBytes(path, bytes);
            return path;
        }

        static private string UpdatePath(string name)
        {
            string path;
            int count = 2;
            do
            {
                path = "C:\\Users\\User\\Desktop\\local repos\\FactoryAPI\\pictures\\" + name + ".png";
                if (System.IO.File.Exists(path))
                {
                    if (!name.Contains('_'))
                        name += $"_{count}";
                    else
                    {
                        name = name.Replace($"_{count - 1}", $"_{count}");
                    }
                }
                count++;
            } while (System.IO.File.Exists(path));
            return path;
        }
        static public byte[] ReadImage(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
