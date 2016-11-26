﻿using System;
using Domain.Events.Speed;

namespace Domain.Model
{
    public class Speed : Entity
    {
        public DateTime Date { get; set; }

        public long SpeedInMilesPerHour { get; set; }

        public bool IsVerified { get; set; }

        public bool IsDeleted { get; set; }

        public string Notes { get; set; }

        public Guid PilotId { get; set; }

        public Guid WitnessId { get; set; }

        public Guid RejectedById { get; set; }

        public Guid VerifiedById { get; set; }

        public Guid SiteId { get; set; }

        public Guid AircraftId { get; set; }

        public void Apply(SpeedClaimCreated createdEvent)
        {
            Date = createdEvent.SpeedClaimedDate;
            SpeedInMilesPerHour = createdEvent.SpeedInMilesPerHour;
            Notes = createdEvent.Notes;
            PilotId = createdEvent.PilotId;
            WitnessId = createdEvent.WitnessId;
            SiteId = createdEvent.SiteId;
            IsVerified = false;
        }

        public void Apply(SpeedClaimVerified verifiedEvent)
        {
            VerifiedById = verifiedEvent.VerifiedById;
            IsVerified = true;
        }

        public void Apply(RecordedSpeedDeleted deletedEvent)
        {
            RejectedById = deletedEvent.DeletedById;
            IsVerified = false;
            IsDeleted = true;
        }
    }
}