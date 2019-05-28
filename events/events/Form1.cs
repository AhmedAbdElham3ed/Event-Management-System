using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Threading;

namespace events
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Thread sli = new Thread(new ThreadStart(sliding));
            sli.Start();
            Thread.Sleep(4900);
            
            InitializeComponent();
            sli.Abort();
            string k;

            char pp = ' ';
            string[] o = new string[7];

            for (int i = 0; i < 7; i++)
                o[i] = "";
            StreamReader readdone = new StreamReader("Done_Events.txt");
            while (!readdone.EndOfStream)
            {

                k = readdone.ReadLine().ToString();
                if (k == "")
                    break;
                Event.@event ew = new Event.@event();
                o = k.Split(pp);
                string m = o[5] + " " + o[6];
                ew.setName(o[0]); ew.setSdate(o[1]); ew.setEdate(o[2]); ew.setPlace(o[3]); ew.setStime(m); ew.setremind(o[4]);
                Event.eventoperations.done_events.Add(ew);
            }
            readdone.Close();
            string s;
            char sp = ' ';
            char splitsdate = '/';
            int year, month, day;
            string[] hh = new string[3];
            string[] h = new string[7];
            StreamReader st = new StreamReader("Events.txt");

            while (!st.EndOfStream)
            {

                s = st.ReadLine().ToString();
                if (s == "")
                    break;
                Event.@event ew = new Event.@event();
                h = s.Split(sp);
                string m = h[5] + " " + h[6];
                ew.setName(h[0]); ew.setSdate(h[1]); ew.setEdate(h[2]); ew.setPlace(h[3]); ew.setStime(m); ew.setremind(h[4]);
                hh = h[1].Split(splitsdate);
                year = Convert.ToInt32(hh[0]); month = Convert.ToInt32(hh[1]); day = Convert.ToInt32(hh[2]);
                DateTime systime = DateTime.Today;


                if ((year < systime.Year) || (year == systime.Year && month < systime.Month) || (year == systime.Year && month == systime.Month && day < systime.Day))
                {
                    Event.eventoperations.done_events.Add(ew);


                }
                else
                {
                    Event.eventoperations.map.Add(h[1], ew);

                    Event.temp t = new Event.temp();
                    t.start_date = ew.getSdate();
                    t.end_date = ew.getEdate();
                    Event.eventoperations.tmp.Add(t);
                }

            }

            st.Close();

            Upcoming a = new Upcoming();
            a.Dock = DockStyle.Fill;
            panel2.Controls.Add(a);
        }

     
        public void sliding()
        {
            Application.Run(new MainForm());
        }
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            SoundPlayer s = new SoundPlayer("multimedia_button_click_012.WAV");
            s.Play();
           /* panel2.Visible = true;
            panel1.Visible = false;
            panel3.Visible = false;

            if (menu11.Width != 50)
            {
                menu11.Visible = false;
                menu11.Width = 50;
                panelTransition.ShowSync(menu11);
            }*/
            panel3.Height = bunifuFlatButton4.Height;
            panel3.Top = bunifuFlatButton4.Top;
            panel2.Controls.Clear();
            Upcoming a = new Upcoming();
            a.Dock = DockStyle.Fill;
            panel2.Controls.Add(a);

        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            SoundPlayer s = new SoundPlayer("Close.WAV");
            s.Play();
            StreamWriter wri = new StreamWriter("Done_Events.txt");
            for (int i = 0; i < Event.eventoperations.done_events.Count; i++)
            {
                wri.WriteLine(Event.eventoperations.done_events[i].getName() + " " + Event.eventoperations.done_events[i].getSdate() + " " + Event.eventoperations.done_events[i].getEdate() + " " + Event.eventoperations.done_events[i].getPlace() + " " + Event.eventoperations.done_events[i].getremind() + " " + Event.eventoperations.done_events[i].getStime());
            }
            wri.Close();
            StreamWriter wr = new StreamWriter("Events.txt");
            foreach (KeyValuePair<string, Event.@event> kvp in Event.eventoperations.map)
            {

                wr.WriteLine(kvp.Value.getName() + " " + kvp.Value.getSdate() + " " + kvp.Value.getEdate() + " " + kvp.Value.getPlace() + " " + kvp.Value.getremind() + " " + kvp.Value.getStime());
            }
            wr.Close();
            DialogResult d2 = MessageBox.Show("Do You Really Want to Exit The Application", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d2 == DialogResult.Yes)
                Application.Exit();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            

            SoundPlayer s = new SoundPlayer("multimedia_button_click_012.WAV");
            s.Play();
            
            panel2.Controls.Clear();
            Addition a = new Addition();
            a.Dock = DockStyle.Fill;
            panel2.Controls.Add(a);
            panel3.Height = bunifuFlatButton3.Height;
            panel3.Top = bunifuFlatButton3.Top;
            
           
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            SoundPlayer s = new SoundPlayer("multimedia_button_click_012.WAV");
            s.Play();
            panel3.Height = bunifuFlatButton1.Height;
            panel3.Top = bunifuFlatButton1.Top;
            panel2.Controls.Clear();
            Done d = new Done();
            d.Dock = DockStyle.Fill;
            panel2.Controls.Add(d);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            
            SoundPlayer s = new SoundPlayer("multimedia_button_click_012.WAV");
            s.Play();
            
            panel2.Controls.Clear();
            Edition ed = new Edition();
            ed.Dock = DockStyle.Fill;
            panel2.Controls.Add(ed);

            panel3.Height = bunifuFlatButton2.Height;
            panel3.Top = bunifuFlatButton2.Top;

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

    }
}
