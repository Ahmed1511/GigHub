using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Models
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime datetime;

            var isvalid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:MM",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out datetime);

            return (isvalid);
        }

    }


}