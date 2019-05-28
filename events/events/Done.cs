using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace events
{
    public partial class Done : UserControl
    {
        public Done()
        {
            InitializeComponent();
            DataTable t = new DataTable();
            t.Columns.Add("Event Name");
            t.Columns.Add("Start Date");
            t.Columns.Add("End Date");
            t.Columns.Add("Place");
            t.Columns.Add("Start Time");
            t.Columns.Add("Reminder Date");
            DateTime s = DateTime.Today;
            DataRow tr;
            for (int i = 0; i < Event.eventoperations.done_events.Count;i++ )
            {
                tr = t.NewRow();
                tr["Event Name"] = Event.eventoperations.done_events.ElementAt<Event.@event>(i).getName();
                tr["Start Date"] = Event.eventoperations.done_events.ElementAt<Event.@event>(i).getSdate();
                tr["End Date"] = Event.eventoperations.done_events.ElementAt<Event.@event>(i).getEdate();
                tr["Place"] = Event.eventoperations.done_events.ElementAt<Event.@event>(i).getPlace();
                tr["Start Time"] = Event.eventoperations.done_events.ElementAt<Event.@event>(i).getStime();
                tr["Reminder Date"] = Event.eventoperations.done_events.ElementAt<Event.@event>(i).getremind();

                t.Rows.Add(tr);
            }
                
            dataGridView1.DataSource = t;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
