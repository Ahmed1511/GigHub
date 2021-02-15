﻿using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModel
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcommingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }

    }


}