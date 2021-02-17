using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModel
{
    public class GigFormViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        //[FutureDate]
        public string Date { get; set; }

        [Required]
        //[ValidTime]
        public string Time { get; set; }

        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

        public string Heading { get; set; }
        public string Action { 
            get
            { 
                return (ID != 0) ? "Update" : "Create"; 
            }

            }


    }
}