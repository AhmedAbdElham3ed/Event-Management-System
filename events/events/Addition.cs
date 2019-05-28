using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace events
{
    public partial class Addition : UserControl
    {
        private  static Addition _instance;
        public  static Addition Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Addition();
                return _instance;
            }
        }

        public Addition()
        {
            InitializeComponent();
            DateTime systime = DateTime.Today;

            dateTimePicker1.MinDate = systime.Date;
            dateTimePicker4.MinDate = systime.Date;
            textBox7.Text = textBox8.Text = dateTimePicker1.Text = dateTimePicker2.Text = dateTimePicker3.Text = dateTimePicker4.Text = "";
        }


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            SoundPlayer sou = new SoundPlayer("Operations.WAV");
            sou.Play();
            string[] edate = new string[3];
            string[] sdate = new string[3];
            int y1, m1, d1, y2, m2, d2;
            bool valid_event = true;
            int index;
            string time;
            string[] tt = new string[2];
            char c = ' ';
            tt = dateTimePicker3.Text.Split(c);

            if (tt[1] == "م")
                time = tt[0] + " " + "PM";
            else
                time = tt[0] + " " + "AM";


            bool validname = true, validplace = true;
            for (int i = 0; i < textBox7.Text.Length;i++ )
            {
                if (textBox7.Text[i] == ' ' && textBox7.Text[i + 1] != ' ')
                {
                    validname = false;
                    break;
                }

            }
            for (int i = 0; i < textBox8.Text.Length; i++)
            {
                if (textBox8.Text[i] == ' ' && textBox8.Text[i + 1] != ' ')
                {
                    validplace = false;
                    break;
                }

            }

                if (textBox7.Text == "" || textBox8.Text == "")
                {
                    MessageBox.Show("You Must Fill Your Event Information", "", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (validname==false||validplace==false)
                {
                    MessageBox.Show("The Name Must Be One Word", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Event.@event eva = new Event.@event(textBox7.Text, dateTimePicker1.Text, dateTimePicker2.Text, textBox8.Text, time, dateTimePicker4.Text);
                    if (Event.eventoperations.map.ContainsKey(dateTimePicker1.Text))
                    {
                        MessageBox.Show("There is an event on " + dateTimePicker1.Text, "Addition Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Event.eventoperations.map.Add(dateTimePicker1.Text, eva);
                        index = Event.eventoperations.map.IndexOfKey(dateTimePicker1.Text);

                        if (Event.eventoperations.map.Count == 1)
                            valid_event = true;
                        else if (index == 0 && Event.eventoperations.map.Count > 1)
                        {

                            edate = eva.getEdate().Split('/');
                            sdate = Event.eventoperations.tmp[index].start_date.Split('/');
                            y1 = Convert.ToInt16(edate[0]);
                            m1 = Convert.ToInt16(edate[1]);
                            d1 = Convert.ToInt16(edate[2]);
                            y2 = Convert.ToInt16(sdate[0]);
                            m2 = Convert.ToInt16(sdate[1]);
                            d2 = Convert.ToInt16(sdate[2]);
                            if ((y1 > y2) || (y1 == y2 && m1 > m2) || (y1 == y2 && m1 == m2 && d1 >= d2))
                                valid_event = false;

                        }
                        else if (index == Event.eventoperations.map.Count - 1 && Event.eventoperations.map.Count > 1)
                        {
                            sdate = eva.getSdate().Split('/');

                            edate = Event.eventoperations.tmp[index - 1].end_date.Split('/');
                            y1 = Convert.ToInt16(sdate[0]);
                            m1 = Convert.ToInt16(sdate[1]);
                            d1 = Convert.ToInt16(sdate[2]);
                            y2 = Convert.ToInt16(edate[0]);
                            m2 = Convert.ToInt16(edate[1]);
                            d2 = Convert.ToInt16(edate[2]);

                            if ((y1 < y2) || (y1 == y2 && m1 < m2) || (y1 == y2 && m1 == m2 && d1 <= d2))
                                valid_event = false;
                        }
                        else
                        {

                            sdate = eva.getSdate().Split('/');

                            edate = Event.eventoperations.tmp[index - 1].end_date.Split('/');
                            y1 = Convert.ToInt16(sdate[0]);
                            m1 = Convert.ToInt16(sdate[1]);
                            d1 = Convert.ToInt16(sdate[2]);
                            y2 = Convert.ToInt16(edate[0]);
                            m2 = Convert.ToInt16(edate[1]);
                            d2 = Convert.ToInt16(edate[2]);

                            if ((y1 < y2) || (y1 == y2 && m1 < m2) || (y1 == y2 && m1 == m2 && d1 <= d2))
                                valid_event = false;

                            else if (valid_event == true)
                            {
                                edate = eva.getEdate().Split('/');
                                sdate = Event.eventoperations.tmp[index].start_date.Split('/');
                                y1 = Convert.ToInt16(edate[0]);
                                m1 = Convert.ToInt16(edate[1]);
                                d1 = Convert.ToInt16(edate[2]);
                                y2 = Convert.ToInt16(sdate[0]);
                                m2 = Convert.ToInt16(sdate[1]);
                                d2 = Convert.ToInt16(sdate[2]);
                                if ((y1 > y2) || (y1 == y2 && m1 > m2) || (y1 == y2 && m1 == m2 && d1 >= d2))
                                    valid_event = false;
                            }
                        }
                        if (valid_event == true)
                        {
                            Event.temp t = new Event.temp();
                            t.start_date = eva.getSdate();
                            t.end_date = eva.getEdate();
                            Event.eventoperations.tmp.Insert(index, t);
                            textBox7.Text = dateTimePicker1.Text = dateTimePicker2.Text = dateTimePicker3.Text = textBox8.Text = dateTimePicker4.Text = "";
                            MessageBox.Show("Added", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Event.eventoperations.map.RemoveAt(index);
                            MessageBox.Show("There is an intersection with another Event", "Addition Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
        }


        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker4.MaxDate = dateTimePicker1.Value;
        }
       

        
    }
}
