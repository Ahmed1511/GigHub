using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int ID { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistID { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }


        public Genre Genre { get; set; }

        [Required]
        public int GenreID { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public void Cancel()
        {
            IsCanceled = true;
            var Notification = new Notification(NoteficationType.GigCanceled, this);

            foreach (var Attendee in Attendances.Select(a => a.Attendee))
            {
                Attendee.Notify(Notification);
            }
        }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }
    }
}