using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DataAccess.Models
{
    public class ModuleModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Version { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public Assembly Assembly { get; set; } = null!;
    }
}
