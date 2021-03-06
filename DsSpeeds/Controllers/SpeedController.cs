﻿using Marten;
using Read.Models;
using StructureMap;
using System;
using System.Linq;
using System.Web.Mvc;
using Commands.Speed;
using Domain.Model;
using DsSpeeds.Models;
using DsSpeeds.Models.Speed;

namespace DsSpeeds.Controllers
{
    public class SpeedController : BaseController
    {
        public SpeedController(IDocumentSession session, IContainer container) : base(session, container)
        {
        }


        [HttpGet]
        public ActionResult AllVerified()
        {
            var model = new SpeedListModel
            {
                SpeedList = DocumentSession.Query<SpeedReadModel>().Where(speed => speed.IsVerified).ToList()
            };

            return View("Index", model);
        }


        [HttpGet]
        public ActionResult AllUnverified()
        {
            var model = new SpeedListModel
            {
                SpeedList = DocumentSession.Query<SpeedReadModel>().Where(speed => !speed.IsVerified).ToList()
            };

            return View("UnverifiedIndex", model);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var recSpeed = DocumentSession.Load<SpeedReadModel>(id);

            var model = new SpeedDetailModel(recSpeed);

            var allEvents = DocumentSession.Events.FetchStream(id)
                .Select(e => $"{e.Timestamp.Date} - {e.Data.GetType().Name}" )
                .ToList();

            model.SpeedHistory = allEvents;

            return View("Details", model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.People = DocumentSession.Query<Person>().Select(p => new SelectListItem { Text = p.UserName, Value = p.Id.ToString() }).ToList();
            ViewBag.Aircraft = DocumentSession.Query<Aircraft>().Select(p => new SelectListItem { Text = p.AircraftName, Value = p.Id.ToString() }).ToList();
            ViewBag.Sites = DocumentSession.Query<Site>().Select(p => new SelectListItem { Text = p.SiteName, Value = p.Id.ToString() }).ToList();

            return View("Create", new CreateSpeedClaimCommand(DocumentSession));
        }

        [HttpPost]
        public ActionResult Create(CreateSpeedClaimCommand speedClaim)
        {
            ExecuteCommand(speedClaim);

            return RedirectToAction("AllUnverified");
        }


        public ActionResult Edit(string id)
        {
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Edit(SpeedClaimModel speed)
        {
            return RedirectToAction("AllVerified");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var command = CreateCommand<DeleteRecordedSpeedCommand>();
            command.Id = id;
            command.DeletedById = CurrentUser;

            ExecuteCommand(command);

            return RedirectToAction("AllVerified");
        }

        [HttpPost]
        public ActionResult Verify(Guid id)
        {
            var command = CreateCommand<VerifySpeedClaimCommand>();
            command.Id = id;

            ExecuteCommand(command);

            return RedirectToAction("Details", new { id });
        }

    }
}