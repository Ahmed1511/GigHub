using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserID { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationID { get; private set; }

        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }
        public bool IsRead { get; set; }

        protected UserNotification()
        {

        }
        public UserNotification(ApplicationUser User, Notification notification)
        {
            if (User == null)

                throw new ArgumentException("User");


            if (notification == null)

                throw new ArgumentNullException("notification");

            this.User = User;
            Notification = notification;
        }


    }
}