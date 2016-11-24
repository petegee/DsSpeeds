﻿using Domain.Events.SpeedClaims;
using System;

namespace Read.Models
{
    public class RecordedSpeedReadModel : BaseReadModel
    {
        public DateTime Date { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public DateTime? DeletionDate { get; set; }

        public long SpeedInMilesPerHour { get; set; }

        public bool IsVerified { get; set; }

        public bool IsDeleted { get; set; }

        public string Notes { get; set; }

        public string PilotName { get; set; }

        public string WitnessName { get; set; }

        public string VerifiedByName { get; set; }

        public string DeletedByName { get; set; }

        public string SiteName { get; set; }

        public string AircraftName { get; set; }


        public void Apply(SpeedClaimCreated speedClaimCreatedEvent)
        {
            Date = speedClaimCreatedEvent.SpeedClaimedDate;
            SpeedInMilesPerHour = speedClaimCreatedEvent.SpeedInMilesPerHour;
            Notes = speedClaimCreatedEvent.Notes;
            WitnessName = speedClaimCreatedEvent.WitnessName;
            PilotName = speedClaimCreatedEvent.PilotName;
            SiteName = speedClaimCreatedEvent.SiteName;
            AircraftName = speedClaimCreatedEvent.AircraftName;
        }

        public void Apply(SpeedClaimVerified speedClaimVerifiedEvent)
        {
            VerifiedDate = speedClaimVerifiedEvent.SpeedVerifiedDate;
            VerifiedByName = speedClaimVerifiedEvent.VerifiedByName;
            IsVerified = true;
        }
    
        public void Apply(RecordedSpeedDeleted deletedEvent)
        {
            DeletionDate = deletedEvent.SpeedDeletionDate;
            DeletedByName = deletedEvent.DeletedByName;
            IsVerified = false;
            IsDeleted = true;
        }

    }
}
