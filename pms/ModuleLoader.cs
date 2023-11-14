using pms.Tools;

namespace pms
{
    public static class ModuleLoader
    {
        public static void RegisterModules(this IMvcBuilder mvcBuilder, IHostEnvironment env)
        {
            var modules = ModuleFinder.Find(env.IsDevelopment() ? env.ContentRootPath : "", "Modules.Employees");

            if (modules.Any())
            {
                foreach (var module in modules)
                {
                    mvcBuilder.AddApplicationPart(module.Assembly);
                }
            }
        }
    }
}
