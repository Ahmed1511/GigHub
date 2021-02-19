using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var UserID = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(a => a.ID == id && a.ArtistID == UserID);
            gig.IsCanceled = true;
            var Notification = new Notification
            {
                DateTime = DateTime.Now,
                Gig = gig,
                Type = NoteficationType.GigCanceled
            };

            var Attendees = _context.Attendances
                .Where(a => a.GigID == gig.ID)
                .Select(a => a.Attendee)
                .ToList();



            foreach (var Attendee in Attendees)
            {
                Attendee.Notify(Notification);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
