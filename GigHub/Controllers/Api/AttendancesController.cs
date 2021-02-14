using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;
        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var UserID = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeID == UserID && a.GigID == dto.GigId))
            {
                return BadRequest("The Attendance Already exist.");
            }
                
            Attendance attendance = new Attendance
            {
                GigID = dto.GigId,
                AttendeeID = UserID
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}
