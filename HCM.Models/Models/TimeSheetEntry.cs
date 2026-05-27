using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HCM.Models.Models
{
    public class TimeSheetEntry
    {
        [Key]
        public int EntryId { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        [Required]
        [Range(0, 24)]
        public decimal LogHours { get; set; }

        // Foreign key to TaskItem
        [Required]
        public int TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]
        public TaskItem Task { get; set; }

        [MaxLength(100)]
        public string SubTask { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        // Optional metadata
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
    }
}
