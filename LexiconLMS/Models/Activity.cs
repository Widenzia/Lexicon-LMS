﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public Activity()
        {
        }

        public Activity(ActivityCreateViewModel activity)
        {
            Name = activity.Name;
            ActivityTypeId = activity.ActivityTypeId;
            Description = activity.Description;
            StartDate = activity.StartDate.Value;
            EndDate = activity.EndDate.Value;
            ModuleId = activity.ModuleId;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }

        // Navigation Properties
        public virtual ActivityType ActivityType { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<Document> Documents { get; set; }

        public void Update(ActivityCreateViewModel activity)
        {
            Name = activity.Name;
            Description = activity.Description;
            ActivityTypeId = activity.ActivityTypeId;
            if (activity.StartDate != null)
                StartDate = activity.StartDate.Value;
            if (activity.EndDate != null)
                EndDate = activity.EndDate.Value;
        }
    }

    public class ActivityType
    {
        public int Id { get; set; }
        public bool IsAssignment { get; set; }
        public string Name { get; set; }
    }

    public class ActivityIndexVieWModel
    {
        public ActivityIndexVieWModel(Module module, IEnumerable<ActivityViewModel> activities)
        {
            ModuleName = module.Name;
            ModuleId = module.Id;
            Activities = activities;
        }

        public string ModuleName { get; }
        public int ModuleId { get; }
        public IEnumerable<ActivityViewModel> Activities { get; }
    }

    public class ActivityScheduleViewModel
    {
        public ActivityScheduleViewModel(Activity activity)
        {
            Id = activity.Id;
            Name = activity.Name;
            StartTime = activity.StartDate.ToString("HH:mm");
        }

        public int Id { get; }
        public string Name { get; }
        public string StartTime { get; }
    }

    public class ActivityViewModel
    {
        public ActivityViewModel(Activity activity)
        {
            Id = activity.Id;
            Name = activity.Name;
            Description = activity.Description;
            StartDate = activity.StartDate;
            EndDate = activity.EndDate;
            ActivityType = activity.ActivityType.Name;
            IsAssignment = activity.ActivityType.IsAssignment;
            DocumentsCount = activity.Documents.Count;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime StartDate { get; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime EndDate { get; }

        [Display(Name = "Activity Type")]
        public string ActivityType { get; }

        public bool IsAssignment { get; }

        [Display(Name = "Activity Documents")]
        public int DocumentsCount { get; }
    }

    public class ActivityDeleteViewModel
    {
        public ActivityDeleteViewModel(Activity activity)
        {
            Name = activity.Name;
            StartDate = activity.StartDate;
            EndDate = activity.EndDate;
            ActivityType = activity.ActivityType.Name;
            ModuleId = activity.ModuleId;
            DocumentsCount = activity.Documents.Count;
        }

        public string Name { get; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime StartDate { get; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime EndDate { get; }

        [Display(Name = "Activity Type")]
        public string ActivityType { get; }

        [Display(Name = "Activity Documents")]
        public int DocumentsCount { get; }

        public int ModuleId { get; }

        public bool HasDocuments => DocumentsCount > 0;

        public string DeleteType
        {
            get
            {
                var sb = new StringBuilder("Activity");
                if (HasDocuments)
                {
                    sb.Append(" and the related Documents");
                }
                return sb.ToString();
            }
        }

    }

    public class ActivityCreateViewModel
    {
        public ActivityCreateViewModel()
        {
        }

        public ActivityCreateViewModel(Module module)
        {
            ModuleId = module.Id;
            ModuleName = module.Name;
            if (StartDate == null)
                StartDate = DateTime.Today.AddHours(value: 8);
            if (EndDate == null)
                EndDate = DateTime.Today.AddHours(value: 17);
        }

        public ActivityCreateViewModel(Activity activity) : this(activity.Module)
        {
            Name = activity.Name;
            Description = activity.Description;
            StartDate = activity.StartDate;
            EndDate = activity.EndDate;
        }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [GenericCompare(CompareToPropertyName = "StartDate", OperatorName = GenericCompareOperator.GreaterThan,
            ErrorMessage = "End Date cannot be before the Start Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        [Display(Name = "Activity Type")]
        public int ActivityTypeId { get; set; }

        public string ModuleName { get; set; }
    }

    public class ActivityDetailsViewModel
    {
        public ActivityDetailsViewModel(Activity activity)
        {
            Name = activity.Name;
            Description = activity.Description;
            Documents = activity.Documents.Select(d => new DocumentViewModel(d));
        }

        public string Name { get; }
        public string Description { get; }
        public IEnumerable<DocumentViewModel> Documents { get; }
    }
}

