﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DsSpeeds.Models.Speed
{
    public class SpeedClaimModel
    {
        public Guid Id { get; set; }

        [Display(Name="Date")]
        public DateTime Date { get; set; }

        [Display(Name="Speed (MPH)")]
        public long SpeedInMilesPerHour { get; set; }

        [Display(Name = "Pilot")]
        public string PilotName { get; set; }

        [Display(Name = "Witness")]
        public string WitnessName { get; set; }

        [Display(Name = "Verified")]
        public bool IsVerified { get; set; }

        [Display(Name = "Place")]
        public string LocationPlaceName { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Plane")]
        public string PlaneName { get; set; }

        [Display(Name = "Span (inches)")]
        public long SpanInInches { get; set; }

        [Display(Name = "Reported by")]
        public string ReportingUserName { get; set; }

        [Display(Name = "Last updated by")]
        public string LastUpdatedBy { get; set; }

        [Display(Name = "Last updated on")]
        public DateTime LastUpdatedOn { get; set; }

        [Display(Name = "Verified by")]
        public string VerifyingUserName { get; set; }

    }
}