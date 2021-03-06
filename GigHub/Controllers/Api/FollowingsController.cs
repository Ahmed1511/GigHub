﻿using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var USerID = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FolloweeID == USerID && f.FolloweeID == dto.FolloweeID))
            {
                return BadRequest("Following is Already Exist");
            }

            Following following = new Following
            {
                FollowerID = USerID,
                FolloweeID = dto.FolloweeID
            };

            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }


    }
}
