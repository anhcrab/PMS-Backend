namespace pms.Tools
{
    public static class ModuleExtracter
    {
        private static readonly string TargetDir = AppDomain.CurrentDomain.BaseDirectory + "Modules";
        private static readonly string SrcDir = AppDomain.CurrentDomain.BaseDirectory + "FileStorage";

        public static string ExtractModule(string filename)
        {
            var DirName = TargetDir + "\\" + filename.Split(".zip")[0];

            if (!Directory.Exists(DirName))
            {
                Directory.CreateDirectory(DirName);
            }

            var zipPath = Directory.GetFiles(SrcDir, filename, SearchOption.AllDirectories).FirstOrDefault();

            if (zipPath != null)
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, DirName);
                return DirName;
            }
            return "";
        }
    }
}
