using DataAccess.Models;
using System.Runtime.Loader;

namespace pms.Tools
{
    public static class ModuleFinder
    {
        public static List<ModuleModel> Find()
        {
            List<ModuleModel> modules = new();

            var moduleRootFolder = new DirectoryInfo(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "terus-content\\modules"
            ));

            if(!moduleRootFolder.Exists)
            {
                Directory.CreateDirectory(moduleRootFolder.ToString());
            }
            var moduleFolders = moduleRootFolder.GetDirectories();

            foreach (var moduleFolder in moduleFolders)
            {
                foreach (var file in moduleFolder.GetFileSystemInfos("*.dll",
                    SearchOption.AllDirectories))
                {
                    modules.Add(new ModuleModel { 
                        Name = file.FullName, 
                        Assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName) 
                    });
                }
            }
            return modules;
        }

        public static List<ModuleModel> Find(string dest, string Name)
        {
            List<ModuleModel> modules = new();

            var moduleRootFolder = dest == "" 
                ? new DirectoryInfo(Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "terus-content\\modules\\" + Name
                ))
                : new DirectoryInfo(Path.Combine(
                    Directory.GetParent(dest)!.Parent!.ToString(),
                    Name + "\\bin"
                ));

            var dlls = moduleRootFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories);

            foreach (var file in dlls)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                modules.Add(new ModuleModel { Name = file.FullName, Assembly = assembly });
            }
            return modules;
        }
    }
}
