using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    public class Candidate
    {
        [Key]
        public int CandidateId
        {
            get; set;
        }
        [Required]
        [Display(Name = "Email:")]
        public string? CandidateEmail { get; set; }
        [Required]

        [Display(Name = "Phone Number:")]
        public string? CandidatePhone { get; set; }


        [Display(Name = "Address:(Optional for primary screening)")]
        public string? CandidateAddress { get; set; }
        [Required]

        [Display(Name = "Gender:")]
        public string? CandidateGender { get; set; }
        [Required]

        [Display(Name = "Candidate Name:")]
        public string? CadidateName { get; set; }
        public string? SkillSets { get; set; }
        [DisplayName("Preferrd Interview Date")]
        public DateOnly InterviewDate { get; set; } = DateOnly.FromDateTime(DateTime.Today); public string? Interviewer { get; set; }
        public DateTime? InterviewerDate { get; set; }
        public string? JobDescription { get; set; }
        [DisplayName("Job Position")]
        public int? JobId { get; set; }
        [ForeignKey("JobId")]
        public Jobs? Jobs { get; set; }
        [Display(Name ="Primary Screening Status")]
        public bool IsSelected { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Secondary Screening Status")]
        public bool IsModified { get; set; }
        public bool IsPreviouslyAttended { get; set; }
        public bool IsUnattended { get; set; }

        public DateTime? ProfileCreatedDate { get; set; }
        [ValidateNever]
        public string? ProfileCreator { get; set; }
        [DisplayName("Upload your CV/ Profile")]
        public string? ProfilePath { get; set; }

        public bool OnboardingRequested { get; set; }
    }
}
