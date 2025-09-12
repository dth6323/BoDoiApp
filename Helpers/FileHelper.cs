namespace BoDoiApp.Helpers
{
    public class FileHelper
    {
        public  string[] ReadDirectoryString(string path)
        {
            try
            {
                string[] directories = System.IO.Directory.GetDirectories(path);
                for (int i = 0; i < directories.Length; i++)
                {
                    directories[i] = System.IO.Path.GetFileName(directories[i]);

                }
                return directories;
            }
            catch
            {
                return new string[] { };

            }
        }
    }
}
