﻿using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            _context.SaveChanges();
            return Ok();
        }
    }
}
