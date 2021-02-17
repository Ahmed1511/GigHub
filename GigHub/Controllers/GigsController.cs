﻿using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            GigFormViewModel viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()

            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Genres = _context.Genres.ToList();
                return View("Create", ViewModel);
            }

            var gig = new Gig
            {
                ArtistID = User.Identity.GetUserId(),
                DateTime = ViewModel.GetDateTime(),
                GenreID = ViewModel.Genre,
                Venue = ViewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var UserID = User.Identity.GetUserId();
            var MyUpcomingGigs = _context.Gigs
                .Where(a => a.ArtistID == UserID && a.DateTime > DateTime.Now)
                .Include(g => g.Genre)
                .ToList();
            return View(MyUpcomingGigs);

        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeID == userId)
                .Select(a => a.Gig)
                .Include(c => c.Artist)
                .Include(c => c.Genre)
                .ToList();
            var gigViewModel = new GigsViewModel
            {
                UpcommingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Going"
            };

            return View("Gigs", gigViewModel);

        }
    }
}