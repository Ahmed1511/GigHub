using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Models
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime datetime;
            var Isvalid = DateTime.TryParseExact(Convert.ToString(value),
                "d mmm yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out datetime);

            return (Isvalid && datetime > DateTime.Now);
        }

    }


}