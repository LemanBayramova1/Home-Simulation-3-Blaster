namespace Home_Simulation_3.Utilities
{
    public static class FileUploadExtension
    {
        public static string SaveImage(this IFormFile imagefile, IWebHostEnvironment env, string folder)
        {
            string path = Path.Combine(env.WebRootPath, folder);
            string filename = Guid.NewGuid().ToString() + "_" + imagefile.FileName;
            string fullpath = Path.Combine(path, filename);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                imagefile.CopyTo(stream);
            }
            return filename;
        }
    }
}
