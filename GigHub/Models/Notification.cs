using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int ID { get;private set; }
        public DateTime DateTime { get;private set; }
        public NoteficationType Type { get;private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        [Required]
        public Gig Gig { get;private set; }

        protected Notification()
        {

        }

        public Notification(NoteficationType type,Gig gig)
        {
            if (gig == null)
            {
                throw new ArgumentNullException("gig");
            }
            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;

        }

    }

   
}