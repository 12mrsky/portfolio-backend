using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string TechStack { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string GithubUrl { get; set; } = string.Empty;
        public string LiveUrl { get; set; } = string.Empty;
    }
    
}
