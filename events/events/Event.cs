using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    struct temp
    {
        public string start_date, end_date;
    }
    class eventoperations
    {
        public static SortedList<string, @event> map = new SortedList<string, @event>();
        public static List<@event> done_events = new List<@event>();
        public static List<temp> tmp = new List<temp>();


    }


    class @event
    {

        string name, start_date, end_date, place, start_time, reminder_time;
        public @event()
        {
            name = start_date = end_date = place = start_time = reminder_time = "";
        }
        public @event(string n, string sd, string ed, string p, string st, string rt)
        {
            name = n;
            start_date = sd;
            end_date = ed;
            place = p;
            start_time = st;
            reminder_time = rt;
        }
        public void setName(string n)
        {
            name = n;
        }
        public string getName()
        {
            return name;
        }
        public void setSdate(string sd)
        {
            start_date = sd;
        }
        public string getSdate()
        {
            return start_date;
        }
        public void setEdate(string ed)
        {
            end_date = ed;
        }
        public string getEdate()
        {
            return end_date;
        }
        public void setPlace(string p)
        {
            place = p;
        }
        public string getPlace()
        {
            return place;
        }
        public void setStime(string st)
        {
            start_time = st;
        }
        public string getStime()
        {
            return start_time;
        }
        public void setremind(string r)
        {
            reminder_time = r;
        }
        public string getremind()
        {
            return reminder_time;
        }

    }
}
