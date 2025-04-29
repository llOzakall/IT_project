using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_modal.Extensions
{
    public static class DateTimeExtensions
    {
        public static int ToAge(this DateTime birthDate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            // Check if the birthday has passed this year. If not, subtract 1 from age
            if (birthDate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
