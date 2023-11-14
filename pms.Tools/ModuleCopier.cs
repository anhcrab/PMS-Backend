namespace pms.Tools
{
    public static class ModuleCopier
    {
        private static readonly string RootDir = AppDomain.CurrentDomain.BaseDirectory;

        public static void FromTo(string from, string to)
        {
            if (Directory.Exists(from))
            {
                if (!Directory.Exists(to))
                {
                    Directory.CreateDirectory(to);
                }
            }
        }
    }
}
