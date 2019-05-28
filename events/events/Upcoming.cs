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
    public partial class Upcoming : UserControl
    {
        public Upcoming()
        {
            InitializeComponent();
           
               
            DataTable t = new DataTable();
            t.Columns.Add("Event Name");
            t.Columns.Add("Start Date");
            t.Columns.Add("End Date");
            t.Columns.Add("Place");
            t.Columns.Add("Start Time");
            t.Columns.Add("Reminder Date");
            t.Columns.Add("Remaining Time");
            DateTime s = DateTime.Today;
            DataRow tr;
            foreach (KeyValuePair<string, Event.@event> kd in Event.eventoperations.map) 
            {
                tr = t.NewRow();
                tr["Event Name"] = kd.Value.getName();
                tr["Start Date"] = kd.Value.getSdate();
                tr["End Date"] = kd.Value.getEdate();
                tr["Place"] = kd.Value.getPlace();
                tr["Start Time"] = kd.Value.getStime();
                tr["Reminder Date"] = kd.Value.getremind();
               tr["Remaining Time"] = Convert.ToDateTime(kd.Key) - s;
                
                t.Rows.Add(tr);


            }
            dataGridView1.DataSource = t;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
