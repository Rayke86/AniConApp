using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniConApp.Model
{
    public class Convention
    {
        public String name { get; set; }
        public List<int> days;
        public String month { get; set; }
        public String year { get; set; }
        public String location { get; set; }
        public String logoUrl { get; set; }

        public Convention(String name, String month, String year, String location, String days)
        {
            this.name = name;
            this.month = month;
            this.year = year;
            this.location = location;
            this.days = this.stringToArray(days);
        }

        public List<int> getDays() { return (days != null) ?  days :  null; }
        public void setDays(List<int> days) { this.days = days; }

        private List<int> stringToArray(String stringDays)
        {
            String[] dayArray = stringDays.Split(',');
            days = new List<int>();

            for(int i = 0; i<dayArray.Length; i++)
            {
                int day = Int32.Parse(dayArray[i]);
                days.Add(day);
            }

            return days;
        }
        
    }
}
