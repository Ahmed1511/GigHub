using GigHub.Models;
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
                Genres = _context.Genres.ToList(),
                Heading = "Create a Gig"

            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Genres = _context.Genres.ToList();
                return View("GigForm", ViewModel);
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
            var Gigs = _context.Gigs
                .Where(a => a.ArtistID == UserID && a.DateTime > DateTime.Now && !a.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
            return View(Gigs);

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

        [Authorize]
        public ActionResult Edit(int id)
        {
            var UserId = User.Identity.GetUserId();
            var Gig = _context.Gigs.Single(a => a.ID == id && a.ArtistID == UserId);
            GigFormViewModel viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Date = Gig.DateTime.ToString("d MMM yyyy"),
                Time = Gig.DateTime.ToString("HH:MM"),
                Genre = Gig.GenreID,
                Venue = Gig.Venue,
                Heading = "Edit a Gig",
                ID = Gig.ID
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Genres = _context.Genres.ToList();
                return View("GigForm", ViewModel);
            }

            var UserID = User.Identity.GetUserId();
            var Gig = _context.Gigs.Single(a => a.ID == ViewModel.ID && a.ArtistID == UserID);
            Gig.Venue = ViewModel.Venue;
            Gig.DateTime = ViewModel.GetDateTime();
            Gig.GenreID = ViewModel.Genre;

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}