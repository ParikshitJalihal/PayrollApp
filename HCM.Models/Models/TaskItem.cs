using System;
using System.ComponentModel.DataAnnotations;

namespace HCM.Models.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public TaskCategory Category { get; set; }
    }

    public enum TaskCategory
    {
        Medium,
        High,
        Critical
    }
}
