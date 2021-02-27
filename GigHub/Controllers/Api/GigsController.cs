using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;

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
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a =>a.Attendee))
                .Single(a => a.ID == id && a.ArtistID == UserID);
            gig.IsCanceled = true;
            var Notification = new Notification(NoteficationType.GigCanceled, gig);

            
            foreach (var Attendee in gig.Attendances.Select(a =>a.Attendee))
            {
                Attendee.Notify(Notification);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
