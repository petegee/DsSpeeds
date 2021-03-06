﻿using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using Data.Queries;
using Domain.Events.Speed;
using Domain.Model;
using Marten;
using Shared;
using Shared.Exceptions;

namespace Commands.Speed
{
    public class CreateSpeedClaimCommand : BaseCommand, ICommand
    {
        public CreateSpeedClaimCommand()
        {
        }

        public CreateSpeedClaimCommand(IDocumentSession docSession) : base(docSession)
        {
        }

        [DisplayName("Date of flight")]
        public DateTime SpeedClaimedDate { get; set; } = DateTime.Today.Date;

        [DisplayName("Max speed of flight (MPH)")]
        public long SpeedInMilesPerHour { get; set; }

        public string Notes { get; set; }

        [DisplayName("Pilot")]
        public Guid PilotId { get; set; }

        [DisplayName("Witness")]
        public Guid WitnessId { get; set; }

        [DisplayName("Site")]
        public Guid SiteId { get; set; }

        [DisplayName("Aircraft")]
        public Guid AircraftId { get; set; }

        public void Validate()
        {
            if (!DocumentSession.Exists<Person>(PilotId))
                throw new BusinessRuleValidationException("Pilot cannot be found. ");

            if (!DocumentSession.Exists<Person>(WitnessId))
                throw new BusinessRuleValidationException("Witness cannot be found. ");

            if (!DocumentSession.Exists<Domain.Model.Site>(SiteId))
                throw new BusinessRuleValidationException("Site cannot be found. ");

            if (!DocumentSession.Exists<Domain.Model.Aircraft>(AircraftId))
                throw new BusinessRuleValidationException("Aircraft cannot be found. ");
        }

        public Guid? Execute()
        {
            var @event = Mapper.Map<SpeedClaimCreated>(this);

            @event.PilotName = DocumentSession.Query<Person>().Single(a => a.Id == PilotId).UserName;
            @event.WitnessName = DocumentSession.Query<Person>().Single(a => a.Id == WitnessId).UserName;
            @event.AircraftName = DocumentSession.Query<Domain.Model.Aircraft>().Single(a => a.Id == AircraftId).AircraftName;

            var site = DocumentSession.Query<Domain.Model.Site>().Single(a => a.Id == SiteId);
            @event.SiteName = site.SiteName;
            @event.SiteLocation = site.Location;
            @event.CountryId = site.CountryId;
            @event.SiteCountryName = DocumentSession.Query<Domain.Model.Country>().Single(a => a.Id == site.CountryId).CountryName;

            var speedRecordId = DocumentSession.Events.StartStream<Domain.Model.Speed>(@event);

            DocumentSession.SaveChanges();

            return speedRecordId;
        }

    }
}
