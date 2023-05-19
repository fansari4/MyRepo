using System.ComponentModel.DataAnnotations;

namespace MyRepo.Models
{
    public class Issue
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime Created { get; set; }
    }

    public enum Priority
    {
        Low,
        Medium,
        Highs
    }
}
