namespace FactoryAPI.Utilities
{
    static public class PictureConverter
    {
        static public string SaveImageGetPath(string[] Base64Images, string name)
        {
            string folderName = "\\pictures\\" + name;
            var directory = Directory.CreateDirectory(Directory.GetCurrentDirectory() + folderName);
            foreach (var Base64Image in Base64Images) 
            {
                byte[] bytes = Convert.FromBase64String(Base64Image);
                string path = UpdatePath(name,directory);
                System.IO.File.WriteAllBytes(path, bytes);
                
            }
            return directory.FullName;
        }

        static private string UpdatePath(string name, DirectoryInfo directoryInfo)
        {
            string path;
            int count = 2;
            do
            {
                path = $"{directoryInfo.FullName}\\{name}.png";
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
        static public List<byte[]> ReadImage(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            var files = directory.GetFiles();
            List<byte[]> result = new List<byte[]>(); 
            foreach (var file in files)
            {
                result.Add(File.ReadAllBytes(file.FullName));
            }
            return result;
        }
    }
}
